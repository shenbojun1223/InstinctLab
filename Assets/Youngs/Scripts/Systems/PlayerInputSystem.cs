using Unity.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;


//@Youngs 2019年4月19日16:13:02 
/*
 *  Tips:   官方API升级 重命名 IJobProcessComponentData - > IJobForEach
 * 
 */
namespace ILab.Youngs
{
    [UpdateBefore(typeof(PlayerMoveSystem))]
    public class PlayerInputSystem : JobComponentSystem
    {
        [BurstCompile]
        struct PlayerInputJob : IJobForEach<PlayerInput>
        {
            public BlittableBool pFire;
            public float moveHorizontal;
            public float moveVertical;

            /*
            *   注意这个进程不在主线程里，因此不要在这写input.pW = Input.GetMouseButton(xxx)
            *   因此delta.time这种东西也要特别注意
            */
            public void Execute(ref PlayerInput pInputData)
            {
                pInputData.inputMovementDirection.x = moveHorizontal;
                pInputData.inputMovementDirection.z = moveVertical;
                pInputData.pFire = pFire;
            }
        }

        //这个是主线程里面做的事
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new PlayerInputJob{

                pFire = Input.GetButtonDown("Fire1"),
                moveHorizontal = Input.GetAxis("Horizontal"),
                moveVertical = Input.GetAxis("Vertical"),
                
            };
            return job.Schedule(this, inputDeps);
        }
    }


}

