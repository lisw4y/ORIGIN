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
        ClearSlot();
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

    public void ShowDetail(int index)
    {
        HUDManager hudManager = HUDManager.instance;
        Inventory inventory = Inventory.instance;

        hudManager.DetailImage.sprite = inventory.items[index].icon;
        if (inventory.items[index].isStackable)
        {
            hudManager.DetailCount.text = inventory.counts[index].ToString();
            hudManager.DetailCount.enabled = true;
        }
        else
        {
            hudManager.DetailCount.enabled = false;
        }
        hudManager.DetailText.text = inventory.items[index].detail;
        if (inventory.items[index].GetType() == typeof(_Material))
        {
            hudManager.DetailUseButton.interactable = false;
            hudManager.DetailShortcutButton1.interactable = false;
            hudManager.DetailShortcutButton2.interactable = false;
            hudManager.DetailShortcutButton3.interactable = false;
        }
        else
        {
            hudManager.DetailUseButton.interactable = true;
            hudManager.DetailShortcutButton1.interactable = true;
            hudManager.DetailShortcutButton2.interactable = true;
            hudManager.DetailShortcutButton3.interactable = true;
            hudManager.DetailUseButton.onClick.AddListener(() => UseItem(index));
            hudManager.DetailShortcutButton1.onClick.AddListener(() => Inventory.instance.PutShortcut(0, index));
            hudManager.DetailShortcutButton2.onClick.AddListener(() => Inventory.instance.PutShortcut(1, index));
            hudManager.DetailShortcutButton3.onClick.AddListener(() => Inventory.instance.PutShortcut(2, index));
        }
        hudManager.ItemDetailPanel.SetActive(true);
    }

    public void UseItem(int index)
    {
        if (item != null)
        {
            item.Use(index);
        }
    }
}
