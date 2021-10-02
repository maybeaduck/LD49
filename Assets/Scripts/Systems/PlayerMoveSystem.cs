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
    
    public class TimerSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<TimeComponent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var time = ref _filter.Get1(item);

                time.DeltaTime = time.StartTime - Time.time;
            }
        }
    }
    
    public class UnstableSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<TimeComponent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);


            }
        }
    }
    
    public class ReactorSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<TimeComponent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);

            }
        }
    }
    
    public class DistructionSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<TimeComponent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);


            }
        }
    }
}