using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public MeshRenderer Preview;
    public Collider Collider;

    private void Start()
    {
        Preview.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Trigger();
        }
    }

    public void Trigger()
    {
        Service<EcsWorld>.Get().NewEntity().Get<FirstTriggerEvent>();
    }
}
