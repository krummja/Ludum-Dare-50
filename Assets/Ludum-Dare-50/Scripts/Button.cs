using System;
using UnityEngine;
using Gameplay;

public class Button : MonoBehaviour
{

    public GameItems ItemType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Inventory.Instance.AddToInventory(ItemType);
        Destroy(gameObject);
    }
}
