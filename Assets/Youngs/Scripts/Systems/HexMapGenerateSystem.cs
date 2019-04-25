using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Burst;
using Unity.Jobs;


//@Youngs 2019年4月24日19:19:15

namespace ILab.Youngs
{
    //创建和销毁实体只能在主线程中进行，避免竞争
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class HexMapGenerateSystem : JobComponentSystem
    {
        //创建指令缓存系统
        BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

        protected override void OnCreate()
        {
            //缓存一下，避免每帧都去创建
            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();

        }

        struct HexCellGenerateJob : IJobForEachWithEntity<HexCellData, LocalToWorld>
        {
            public EntityCommandBuffer CommandBuffer;

            public void Execute(Entity entity, int index,
                [ReadOnly] ref HexCellData hexCellData,
                [ReadOnly] ref LocalToWorld location)
            {
                for (int z = 0, i = 0; z < hexCellData.mapHeight; z++)
                {
                    for (int x = 0; x < hexCellData.mapWidth; x++)
                    {
                        //Create 
                        var instance = CommandBuffer.Instantiate(hexCellData.entity);

                        // Place
                        Vector3 position;
                        position.x = x * 10f;
                        position.y = 0f;
                        position.z = z * 10f;

                        /* ***************************************************************************** */
                        /* 在原型被转化为实体时所有应当具备的组件都具备了，不要自作聪明再添加            */
                        /* 若是出现了实体的确被创建但是组件丢失，注意它必须是从资源管理器中添加到Proxy中 */
                        /* 如果你遇到了问题，看这行代码；如果还是不懂，就去问杨思                        */
                        /* 如果你就是杨思本人，那自闭                                                    */
                        /* ***************************************************************************** */
                        CommandBuffer.SetComponent(instance, new Translation
                        {
                            Value = position,
                        });
                        //CommandBuffer.SetComponent(instance, new HexCellData
                        //{
                        //    id = i,
                        //});
                    }
                }

                CommandBuffer.DestroyEntity(entity);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new HexCellGenerateJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
            }.ScheduleSingle(this, inputDeps);

            m_EntityCommandBufferSystem.AddJobHandleForProducer(job);

            return job;
        }


    }
}
