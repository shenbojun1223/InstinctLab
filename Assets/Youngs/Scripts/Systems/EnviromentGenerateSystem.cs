using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Burst;
using Unity.Jobs;
using static ILab.Youngs.ILabMathmatics;

//@Youngs 2019年4月29日17:40:18

namespace ILab.Youngs
{
    [UpdateAfter(typeof(TopologyProcessSystem))]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class EnviromentGenerateSystem : JobComponentSystem
    {
        BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

        protected override void OnCreate()
        {
            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();

        }

        [ExcludeComponent(typeof(TopographyProcessed))]
        struct TopographyGenerateJob : IJobForEachWithEntity<HexCellData, TopographyProcessRequire, Translation>
        {
            public EntityCommandBuffer CommandBuffer;


            public void Execute(Entity entity, int index,
                [ReadOnly] ref HexCellData hexCellData,
                           ref TopographyProcessRequire topographyProcessRequire,
                [ReadOnly] ref Translation translation)
            {

                var noisePos = _positionVibrate(new float3(translation.Value));
                if (hexCellData.existTree != 0)
                {

                    var woods = CommandBuffer.Instantiate(topographyProcessRequire.wood);

                    // 设置该树林在该地块上
                    CommandBuffer.SetComponent(woods, new Translation
                    {
                        Value = noisePos,
                    });
                }

                CommandBuffer.AddComponent(entity, new TopographyProcessed { });
            }
        }


        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new TopographyGenerateJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),

            }.ScheduleSingle(this, inputDeps);

            m_EntityCommandBufferSystem.AddJobHandleForProducer(job);

            return job;
        }

        
    }
}
