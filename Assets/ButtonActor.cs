using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

public class ButtonActor : MonoBehaviour
{
    private EcsEntity entity;
    public Animator animator;
    public bool active;

    void Start()
    {
        entity = GetComponent<Actor>().actor;
        entity.Get<ButtonData>().actor = this;
    }


}

internal struct ButtonData
{
    public ButtonActor actor;
}
