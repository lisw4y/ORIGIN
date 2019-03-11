using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySlot : MonoBehaviour
{
    public Image icon;
    public Text countText;

    Item item;

    public void CreateSlot(Item newItem, int count, bool isEnough)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        countText.text = count.ToString();
        countText.color = isEnough ? Color.black : Color.red;
        countText.enabled = item.isStackable;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        countText.enabled = false;
    }
}
