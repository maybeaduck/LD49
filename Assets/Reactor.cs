using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using Zlodey;

public class Reactor : MonoBehaviour
{
    private EcsEntity entity;
    public Animator animator;

    void Start()
    {
        entity = GetComponent<Actor>().actor;
        entity.Get<ReactorData>().actor = this;
        entity.Get<TimeComponent>();
        
    }
}

internal struct ReactorData
{
    public Reactor actor;
    public float Time;
    
}
