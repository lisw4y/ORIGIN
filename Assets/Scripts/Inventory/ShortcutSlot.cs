using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutSlot : MonoBehaviour
{
    public Image icon;
    public Text countText;

    Item item;

    public void CreatSlot(Item newItem, int count)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        countText.text = count.ToString();

        if (item.isStackable)
            countText.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        countText.enabled = false;
    }

    public void UseItem(int index)
    {
        if (item != null)
        {
            item.Use(index);
        }
    }
}
