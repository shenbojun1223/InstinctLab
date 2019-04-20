using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;

//@ILab.Youngs 2019年4月19日16:13:02 
namespace ILab.Youngs
{
    [UpdateAfter(typeof(PlayerInputSystem))]
    public class PlayerMoveSystem : JobComponentSystem
    {

        [BurstCompile]
        struct PlayerMoveJob : IJobForEach<PlayerInput, PlayerData, Translation>
        {
            public float dt;

            public void Execute([ReadOnly] ref PlayerInput pInput, ref PlayerData pData, ref Translation translation)
            {
                translation.Value += (pData.moveSpeed * pInput.inputMovementDirection * dt);
            }

        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new PlayerMoveJob
            {
                dt = Time.deltaTime,
            };

            return job.Schedule(this, inputDeps);

        }
    }


}

