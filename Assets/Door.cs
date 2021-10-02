using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator Animator;
    public Collider Collider;

    private void Start()
    {
        Close(); 
    }

    public void Close()
    {
        Animator.SetTrigger("Closed");
        Collider.enabled = true;
    }
    public void Open()
    {
        Animator.SetTrigger("Open");
        Collider.enabled = false;
    }
}
