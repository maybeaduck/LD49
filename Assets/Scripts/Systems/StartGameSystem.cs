using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class StartGameSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<StartGameEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);

                _world.NewEntity().Get<ChangeGameStateEvent>().State = GameState.Play;
                _runtimeData.StartLevelTime = Time.time;

                entity.Destroy();
            }
        }
    }
}