using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text count;
    public Button removeButton;
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        count.text = item.getCount().ToString();

        if (item.isStackable)
            count.enabled = true;

        removeButton.interactable = true;
    }

    public void ShowItem(Item item, bool hasItem, bool enough)
    {
        ClearSlot();
        AddItem(item);
        if (!hasItem)
        {
            count.text = "0";
        }
        if (!enough)
        {
            count.color = Color.red;
        }
        else
        {
            count.color = Color.black;
        }
        removeButton.enabled = false;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        count.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            print(GetComponent<Transform>().parent);
            if (GetComponent<Transform>().parent.name.Equals("ItemsParent"))
            {
                item.Use();
            }
        }
    }
}
