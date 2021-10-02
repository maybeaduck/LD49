using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zlodey;

public class Bridge : MonoBehaviour
{
    public DistructionState TargetDistructionState;
    public void Fracture()
    {
        var state = Service<RuntimeData>.Get().CurrentDistructionState;
        if (state == TargetDistructionState)
        {
            Debug.Log("Fracture");
        }
    }
}
