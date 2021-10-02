using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class MoveCameraToPlayerSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PlayerComponent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var player = ref _filter.Get1(item).Player;

                //position
                var cameraRig = _sceneData.CameraRig;
                cameraRig.transform.position = player.Head.position;

                //rotation horizontal
                var rotation = cameraRig.Rotation.eulerAngles;
                rotation.y = player.transform.rotation.eulerAngles.y;
                cameraRig.Rotation.rotation = Quaternion.Euler(rotation);

                //rotation vertical
                var rotationY = _runtimeData.RotationYRaw;
                var cameraRotation = cameraRig.Camera.transform.rotation.eulerAngles;
                cameraRotation.x -= rotationY;
                cameraRig.Camera.transform.rotation = Quaternion.Euler(cameraRotation);
            }
        }
    } 
}