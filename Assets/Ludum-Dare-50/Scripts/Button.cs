using System;
using UnityEngine;


public class Button : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
    }
}
