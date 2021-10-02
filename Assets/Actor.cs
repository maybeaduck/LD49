using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public EcsEntity actor;

    private void Start()
    {
        actor = Service<EcsWorld>.Get().NewEntity();
        actor.Get<ActorData>();
    }
}

internal struct ActorData
{
}
