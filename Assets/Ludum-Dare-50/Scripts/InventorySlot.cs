using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public int slot;
    public Sprite[] Items;
    public SpriteRenderer icon;
    public SpriteRenderer background;

    private void Update()
    {
        int i = slot-1;
            if (Inventory.Instance.InventorySlots[i] == 0)
            {
                icon.sprite = Items[0];
                background.sprite = Items[0];
            }
            if (Inventory.Instance.InventorySlots[i] != 0)
            {
                // icon.sprite = Items[];
                background.sprite = Items[17];
            }
    }
}
