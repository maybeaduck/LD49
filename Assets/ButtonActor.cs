using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

public class ButtonActor : MonoBehaviour
{
    public EcsEntity entity;
    public Animator animator;
    public bool active;
    public bool Lock;
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
