
using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class InteractSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<InteractEvent, ButtonData> _filter;
        public void Run()
        {
            Ray ray = _sceneData.CameraRig.Camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin,ray.direction * _staticData.distanceToInteract);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.distance <= _staticData.distanceToInteract)
                {
                    if (hit.collider.attachedRigidbody)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            hit.collider.attachedRigidbody.GetComponent<Actor>().actor.Get<InteractEvent>();
                        }
                        _ui.E.gameObject.SetActive(true);
                        Debug.Log("worK");
                    
                    }
                    
                }
                else
                {
                    _ui.E.gameObject.SetActive(false);
                }    
                
            }
            

            foreach (var item in _filter)
            {
                ref var buttonActor = ref _filter.Get2(item).actor;
                if (!buttonActor.Lock  )
                {
                    buttonActor.active = !buttonActor.active;
                    buttonActor.animator.SetBool("Enable",buttonActor.active);
                    _world.NewEntity().Get<FirstTriggerEvent>();    
                }
                else if(!buttonActor.entity.Has<LockFlag>())
                {
                    buttonActor.active = !buttonActor.active;
                    buttonActor.animator.SetBool("Enable",buttonActor.active);
                    _world.NewEntity().Get<FirstTriggerEvent>();
                    buttonActor.entity.Get<LockFlag>();
                }
                
            }
        }
    }

    internal struct LockFlag
    {
    }
}