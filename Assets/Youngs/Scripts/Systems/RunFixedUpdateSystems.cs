using Unity.Entities;
using UnityEngine;

namespace ILab.Youngs
{
    public class RunFixedUpdateSystems : MonoBehaviour
    {

        private CameraFollowSystem cameraFollowSystem;

        private void Start()
        {
            cameraFollowSystem = World.Active.GetOrCreateSystem<CameraFollowSystem>();
        }

        private void FixedUpdate()
        {
            cameraFollowSystem.Update();
        }
    }

}
