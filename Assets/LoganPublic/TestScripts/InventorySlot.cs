using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public int slot;
    public Sprite[] Items;
    public SpriteRenderer icon;
    public SpriteRenderer background;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = slot-1;
            if (Gamestate.inventory[i] == 0)
            {
                icon.sprite = Items[0];
                background.sprite = Items[0];
            }
            if (Gamestate.inventory[i] != 0)
            {
                icon.sprite = Items[Gamestate.inventory[i]];
                background.sprite = Items[17];
            }
    }
}
