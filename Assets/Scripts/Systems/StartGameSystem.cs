using Leopotam.Ecs;

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

                entity.Destroy();
            }
        }
    }
}