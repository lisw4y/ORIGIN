using UnityEngine;
using UnityEngine.UI;

public class ItemDetailSlot : MonoBehaviour
{
    public Image icon;
    public Text countText;
    public Text detailText;

    Item item;

    public void CreateSlot(Item newItem, int count)
    {
        item = newItem;
        icon.sprite = item.icon;
        countText.text = count.ToString();
        countText.color = Color.black;
        countText.enabled = item.isStackable;
        detailText.text = newItem.detail;
    }

    public void ClearSlot()
    {
        if (item != null)
        {
            countText.text = "0";
            countText.color = Color.red;
            HUDManager hudManager = HUDManager.instance;
            hudManager.DetailUseButton.interactable = false;
            hudManager.DetailShortcutButton1.interactable = false;
            hudManager.DetailShortcutButton2.interactable = false;
            hudManager.DetailShortcutButton3.interactable = false;
        }
    }
}
