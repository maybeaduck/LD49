using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Door Door;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter {other.gameObject.name}");

        if (other.CompareTag("Player"))
        {
            Door.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"OnTriggerExit {other.gameObject.name}");

        if (other.CompareTag("Player"))
        {
            Door.Close();
        }
    }
}
