using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class PlayerInputSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, MoveComponent, InputComponent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var player = ref _filter.Get1(item).Player;
                ref var direction = ref _filter.Get2(item).Direction;

                direction = _runtimeData.Direction;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    entity.Get<JumpFlag>();
                }
                
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    entity.Get<SprintFlag>();
                }
            }
        }
    }
}