using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zlodey;

public class DoorTrigger : MonoBehaviour
{
    public Door Door;
    public ZoneType Type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Door.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Door.Close();
        }
    }
}
