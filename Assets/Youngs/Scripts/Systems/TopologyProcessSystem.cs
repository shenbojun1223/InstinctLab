using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Burst;
using Unity.Jobs;
using Random = Unity.Mathematics.Random;
using System;

//@Youngs 2019年4月29日10:30:11

namespace ILab.Youngs
{
    [UpdateAfter(typeof(HexMapGenerateSystem))]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class TopologyProcessSystem : ComponentSystem
    {
        private EntityQuery mapQuery;
        private Random randomGenerator;

        private float delayTimer = 0f;

        protected override void OnCreate()
        {
            //查询器
            mapQuery = GetEntityQuery(
                new EntityQueryDesc
                {
                    None = new ComponentType[] { typeof(TopographyProcessed) },
                    All = new ComponentType[] { typeof(TopographyProcessRequire), typeof(HexCellData) },
                });

            //随机种子生成
            byte[] randomBytes = new byte[4];
            new System.Random().NextBytes(randomBytes);
            randomGenerator = new Random((uint)System.BitConverter.ToUInt32(randomBytes, 0));
            
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            if (mapQuery.CalculateLength() == 0)
            {
                return;
            }
            NativeArray<Entity> cell = mapQuery.ToEntityArray(Allocator.TempJob);

            var woodsCoverRate = ILabBootstrap.Settings.forestCoverRate;
          
            //后期这里应该同时处理许多地形要求
            for (int i = 0; i < cell.Length; i++)
            {
                if ((randomGenerator.NextInt(0, 10) < woodsCoverRate))
                {
                    PostUpdateCommands.SetComponent(cell[i], new HexCellData
                    {
                        existTree = 1,
                    });
                }
            }
            cell.Dispose();
        }
    }
}



//Chunk写法
/*
 * 
 * 
  ArchetypeChunkComponentType<TopographyProcessRequire> TopographyComponentDataRO = GetArchetypeChunkComponentType<TopographyProcessRequire>(true);
            ArchetypeChunkComponentType<HexCellData> HexCellDataRW = GetArchetypeChunkComponentType<HexCellData>(false);

            NativeArray<ArchetypeChunk> mapDataChunkArray = mapQuery.CreateArchetypeChunkArray(Allocator.TempJob);
            if (mapDataChunkArray.Length == 0)
            {
                mapDataChunkArray.Dispose();
                return;
            }

            for (int chunkIndex = 0; chunkIndex < mapDataChunkArray.Length; chunkIndex++)
            {
                ArchetypeChunk chunk = mapDataChunkArray[chunkIndex];
                int dataCount = chunk.Count;

                NativeArray<HexCellData> HexCellDataArray = chunk.GetNativeArray(HexCellDataRW);

                for (int dataIndex = 0; dataIndex < dataCount; dataIndex++)
                {
                    HexCellData hexCellDataArray = HexCellDataArray[dataIndex];

                    while (delayTimer < 1f)
                    {
                        delayTimer += Time.deltaTime;
                    }
                    HexCellData mapInfo = new HexCellData
                    {
                        existTree = (randomGenerator.NextFloat(0f, 1f) > 0.2f) ? 1 : 0,
                    };
                   
                }
            }
            mapDataChunkArray.Dispose();

        }
    }



 * 
 */
