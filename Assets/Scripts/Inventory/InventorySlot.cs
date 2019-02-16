using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text countText;
    public Button removeButton;
    Item item;

    public void CreatSlot(Item newItem, int count)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        countText.text = count.ToString();

        if (item.isStackable)
            countText.enabled = true;

        removeButton.interactable = true;
    }

    public void CreatSlot(Item item, int count, bool isEnough)
    {
        CreatSlot(item, count);
        countText.color = isEnough ? Color.black : Color.red;
        removeButton.interactable = false;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        countText.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton(int index)
    {
        Inventory.instance.Remove(index);
    }

    public void UseItem(int index)
    {
        if (item != null)
        {
            item.Use(index);
        }
    }
}
