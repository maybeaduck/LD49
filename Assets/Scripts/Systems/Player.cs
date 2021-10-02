using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using UnityEngine;

namespace Zlodey
{
    public class Player : MonoBehaviour
    {
        public EcsEntity Entity;
        public CharacterController Controller;
        public PlayerData Data;

        //points
        public Transform Head;

        //render
        public MeshRenderer MeshRenderer;

        private IEnumerator Start()
        {
            yield return null;
            Entity = Service<EcsWorld>.Get().NewEntity();
            Entity.Get<PlayerComponent>().Player = this;
            Entity.Get<InputComponent>();
            Entity.Get<MoveComponent>() = new MoveComponent
            {
                Speed = Data.Speed,
                Gravity = Data.Gravity,
            };

            Service<RuntimeData>.Get().PlayerEntity = Entity;

            //
            MeshRenderer.enabled = false;
        }
    }
}