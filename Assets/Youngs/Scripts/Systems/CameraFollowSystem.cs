using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

//@Youngs 2019年4月22日21:11:44

namespace ILab.Youngs
{
   
    public class CameraFollowSystem : ComponentSystem
    {
        private EntityQuery query;

        private bool firstFrame = true;
        private Vector3 offset;

        protected override void OnCreate()
        {
            query = GetEntityQuery(
                ComponentType.ReadOnly<Translation>(),
                ComponentType.ReadOnly<PlayerData>(),
                ComponentType.ReadOnly<PlayerInput>());
        }

        protected override void OnUpdate()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
                return;

            Entities.With(query).ForEach(
                (Entity entity, ref Translation translation, ref PlayerInput data) =>
                {
                    Vector3 playerPos = translation.Value;

                    if (firstFrame)
                    {
                        offset = mainCamera.transform.position - playerPos;
                        firstFrame = false;
                    }
                    var smoothing = 5f;
                    var dt = Time.deltaTime;
                    var targetCamPos = playerPos + offset;
                    mainCamera.transform.position =Vector3.Lerp(mainCamera.transform.position, targetCamPos, smoothing * dt);
                });
        }
    }
}
