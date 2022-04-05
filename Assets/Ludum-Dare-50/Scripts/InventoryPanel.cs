using UnityEngine;


public class InventoryPanel : MonoBehaviour
{
    public GameObject SlotPrefab;

    public InventorySlot[] Slots;

    private void Start()
    {
        Slots = new InventorySlot[16];

        for ( int i = 0; i < 16; i++ )
        {
            GameObject slot = Instantiate(SlotPrefab);
            slot.transform.parent = transform;
            slot.transform.localPosition = new Vector3(48 + (64 * i), -48, 0);
            Slots[i] = slot.GetComponent<InventorySlot>();
        }
    }
}
