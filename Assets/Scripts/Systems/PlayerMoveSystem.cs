using Leopotam.Ecs;
using UnityEngine;
namespace Zlodey
{
    public class PlayerMoveSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, MoveComponent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var player = ref _filter.Get1(item).Player;
                ref var move = ref _filter.Get2(item);

                var direction = (player.transform.right * move.Direction.x + player.transform.forward * move.Direction.y).normalized;
                var speed = move.Speed;
                var velocity = direction * speed * Time.deltaTime;
                velocity.y = move.DirectionY * Time.deltaTime;

                player.Controller.Move(velocity);
            }
        }
    }

    public class PlayerJumpSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, MoveComponent> _filter;
        private bool _canDoubleJump;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var player = ref _filter.Get1(item).Player;
                ref var move = ref _filter.Get2(item);

                if (player.Controller.isGrounded)
                {
                    _canDoubleJump = true;
                    move.DirectionY = 0f;

                    if (entity.Has<JumpFlag>())
                    {
                        Debug.Log("Jump");

                        move.DirectionY = player.Data.JumpSpeed;
                        entity.Get<JumpEvent>();
                    }
                }
                else
                {
                    if (_canDoubleJump && entity.Has<JumpFlag>())
                    {
                        Debug.Log("DoubleJump");

                        move.DirectionY = player.Data.JumpSpeed;
                        _canDoubleJump = false;
                        entity.Get<JumpEvent>();
                    }
                }

                move.DirectionY -= move.Gravity * Time.deltaTime;
            }
        }
    }
    public class PlayerSpeedSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, MoveComponent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var player = ref _filter.Get1(item).Player;
                ref var move = ref _filter.Get2(item);

                var speed = player.Data.Speed;
                if (entity.Has<SprintFlag>()) move.Speed = speed * 2f;
                else move.Speed = speed;
            }
        }
    }

    public class PlayerRotateSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, InputComponent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var player = ref _filter.Get1(item).Player;
                ref var input = ref _filter.Get2(item);

                var rotationX = _runtimeData.RotationXRaw + player.transform.eulerAngles.y;
                var rotation = new Vector3(0f, rotationX, 0f);

                player.transform.eulerAngles = rotation;
            }
        }
    }
    //public class PlayerCameraRotateSystem : Injects, IEcsRunSystem
    //{
    //    private EcsFilter<PlayerComponent, InputComponent> _filter;
    //    private float _cameraRotationX;
    //    public void Run()
    //    {
    //        foreach (var item in _filter)
    //        {
    //            ref var entity = ref _filter.GetEntity(item);
    //            ref var player = ref _filter.Get1(item).Player;
    //            ref var input = ref _filter.Get2(item);

    //            var cameraRig = _sceneData.CameraRig;
    //            var camera = cameraRig.Camera;
    //            var min = cameraRig.Data.RotationMin;
    //            var max = cameraRig.Data.RotationMax;

    //            _cameraRotationX = Mathf.Clamp(_cameraRotationX - input.RotationYRaw, min, max);
    //            var cameraRigRotation = new Vector3(_cameraRotationX, camera.rotation.eulerAngles.y, camera.rotation.eulerAngles.z);
    //            camera.rotation = Quaternion.Euler(cameraRigRotation);
    //        }
    //    }
    //}
}