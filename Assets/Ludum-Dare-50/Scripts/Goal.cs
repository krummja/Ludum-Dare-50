using System;
using UnityEngine;
using Gameplay;

public class Goal : MonoBehaviour
{

    public ClosureEnum ClosureType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.Sabotage(1);
    }
}
