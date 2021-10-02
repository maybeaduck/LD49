using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

public class ButtonActor : MonoBehaviour
{
    public EcsEntity entity;
    public Animator animator;
    void Start()
    {
        entity = Service<EcsWorld>.Get().NewEntity();
        entity.Get<ButtonData>().actor = this;
    }


}

internal struct ButtonData
{
    public ButtonActor actor;
}
