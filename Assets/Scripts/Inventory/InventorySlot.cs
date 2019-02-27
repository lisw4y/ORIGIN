using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text countText;
    public Button removeButton;

    Item item;

    public void CreateSlot(Item newItem, int count)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        countText.text = count.ToString();
        countText.enabled = item.isStackable;
        removeButton.interactable = true;
    }

    public void CreateSlot(Item item, int count, bool isEnough)
    {
        CreateSlot(item, count);
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

        inventory.itemDetail = index;
        inventory.onItemChangedCallback.Invoke();
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
            hudManager.DetailUseButton.onClick.RemoveAllListeners();
            hudManager.DetailShortcutButton1.onClick.RemoveAllListeners();
            hudManager.DetailShortcutButton2.onClick.RemoveAllListeners();
            hudManager.DetailShortcutButton3.onClick.RemoveAllListeners();
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
