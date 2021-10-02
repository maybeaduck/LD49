using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zlodey;
using DinoFracture;

public class Bridge : MonoBehaviour
{
    public DistructionState TargetDistructionState;
    public CustomPreFracturedGeometry FracturedGeometry;
    public void Fracture()
    {
        var state = Service<RuntimeData>.Get().CurrentDistructionState;
        if (state == TargetDistructionState)
        {
            FracturedGeometry.Fracture();
        }
    }
}
