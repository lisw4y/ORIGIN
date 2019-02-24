using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemsParent;

    Inventory inventory;
    InventorySlot[] inventorySlots;
    ShortcutSlot[] shortcutSlots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();
        shortcutSlots = HUDManager.instance.shortcutPanel.GetComponentsInChildren<ShortcutSlot>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                inventorySlots[i].CreatSlot(inventory.items[i], inventory.counts[i]);
            } else
            {
                inventorySlots[i].ClearSlot();
            }
        }

        for (int i = 0; i < shortcutSlots.Length; i++)
        {
            if (inventory.shortcuts[i] != -1)
            {
                shortcutSlots[i].CreatSlot(inventory.items[inventory.shortcuts[i]], inventory.counts[inventory.shortcuts[i]]);
            }
            else
            {
                shortcutSlots[i].ClearSlot();
            }
        }
    }
}
