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

                time.DeltaTime = Time.time - time.StartTime;
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
        private EcsFilter<SelfDestructionComponent, TimeComponent> _timeFilter;
        private EcsFilter<FirstTriggerEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);

                _world.NewEntity().Get<MonitorScreenSwitchEvent>().State = DistructionState.Phase1;
                _world.NewEntity().Get<ChangeDistructionStateEvent>().State = DistructionState.Phase1;
                var selfDestruction = _world.NewEntity();
                    selfDestruction.Get<SelfDestructionComponent>();
                    selfDestruction.Get<TimeComponent>().StartTime = Time.time;
            }

            foreach (var item in _timeFilter)
            {
                ref var entity = ref _timeFilter.GetEntity(item);
                var time = _timeFilter.Get2(item);
                var startTimeToDestruction = _staticData.StartTimeToDestruction;
                var timeToDestruction =  Mathf.Clamp(startTimeToDestruction - time.DeltaTime,0, startTimeToDestruction);
                _runtimeData.TimeToDestruction = timeToDestruction;
                _runtimeData.TimeToDestructionNormalize =  timeToDestruction / startTimeToDestruction;
            }
        }
    }
    
    public class ChangeDistructionStateSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<ChangeDistructionStateEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);


                var state = _filter.Get1(item).State;
                switch (state)
                {
                    case DistructionState.Start:
                        break;
                    case DistructionState.Phase1:
                        break;
                    case DistructionState.Phase2:
                        break;
                    case DistructionState.Phase3:
                        break;
                    case DistructionState.End:
                        break;
                    default:
                        break;
                }

                _runtimeData.CurrentDistructionState = state;
                entity.Destroy();
            }
        }
    }
    
    public class CheckDistructionStateSystem : Injects, IEcsRunSystem
    {
        public void Run()
        {
            var destructionNormalize = _runtimeData.TimeToDestructionNormalize;
            var stableToPhase2 = _staticData.StableToPhase2;
            var stableToPhase3 = _staticData.StableToPhase3;

            if (destructionNormalize < stableToPhase3)
            {
                var state = DistructionState.Phase2;

                //if (_runtimeData.CurrentDistructionState !=)
                //{
                //    _world.NewEntity().Get<ChangeDistructionStateEvent>().State = state;
                //}
                return;
            }

            if (destructionNormalize < stableToPhase2)
            {
                var state = DistructionState.Phase3;
                _world.NewEntity().Get<ChangeDistructionStateEvent>().State = state;
                return;
            }
        }
    }
    public class TimerUISystem : Injects, IEcsRunSystem
    {
        private EcsFilter<SelfDestructionComponent, TimeComponent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                var timeToDestruction = _runtimeData.TimeToDestruction;
                var seconds = Mathf.Floor(timeToDestruction % 60);
                var minutes = Mathf.Floor((timeToDestruction / 60 ) % 60);

                var secondsText = seconds < 10 ? $"0{seconds}": $"{seconds}";
                var minutesText = minutes < 10 ? $"0{minutes}" : $"{minutes}";
                var timerText = $"{minutesText} : {secondsText}";
                _sceneData.MonitorUI.TimerScreen.TimerText.text = timerText;

                var procent = Mathf.Clamp(_runtimeData.TimeToDestructionNormalize * 100, 0, 100);
                var stableText = $"stable {Mathf.Floor(procent)}";
                _sceneData.MonitorUI.TimerScreen.StableText.text = stableText;
                _sceneData.MonitorUI.TimerScreen.StableSlider.maxValue = 100;
                _sceneData.MonitorUI.TimerScreen.StableSlider.value = procent;
            }
        }
    }
    
    public class StartDistructionSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<FirstTriggerEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);

                _world.NewEntity().Get<MonitorScreenSwitchEvent>().State = DistructionState.Phase1;
            }
        }
    }
    
    public class MonitorScreenSwitcherSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<MonitorScreenSwitchEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                var monitorUI = _sceneData.MonitorUI;
                var state = _filter.Get1(item).State;
                switch (state)
                {
                    case DistructionState.Start:
                        monitorUI.TimerScreen.Hide();
                        monitorUI.WarningScreen.Hide();
                        monitorUI.WelcomScreen.Show();
                        break;
                    case DistructionState.Phase1:
                        monitorUI.TimerScreen.Show();
                        monitorUI.WarningScreen.Show();
                        monitorUI.WelcomScreen.Hide();
                        break;
                    case DistructionState.Phase2:
                        break;
                    case DistructionState.Phase3:
                        break;
                    case DistructionState.End:
                        break;
                    default:
                        break;
                }

                entity.Destroy();
            }
        }
    } 
}