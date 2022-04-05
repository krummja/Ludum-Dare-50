using System;
using UnityEngine;
using Gameplay;

public class Button : MonoBehaviour
{
    public GameItems ItemType;
    private bool isPickedUp = false;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        if (isPickedUp) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Inventory.Instance.AddToInventory(ItemType);
        isPickedUp = true;
    }
}
