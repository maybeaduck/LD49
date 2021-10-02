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
                velocity.y = move.Gravity * Time.deltaTime;

                player.Controller.Move(velocity);
            }
        }
    } 
}