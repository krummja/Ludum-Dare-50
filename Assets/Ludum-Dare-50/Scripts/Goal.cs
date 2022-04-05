using System;
using UnityEngine;
using Gameplay;

public class Goal : MonoBehaviour
{
    public ClosureEnum ClosureType;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.Sabotage(ClosureType);
    }
}
