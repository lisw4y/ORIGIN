using UnityEngine;
using UnityEngine.UI;

public class DroppedItem : Interactable
{
    public Item item;
    
    void Start()
    {
        interactableType = InteractableType.DroppedItem;
    }

    public override void ShowInfo()
    {
        SetInfo();
        HUDManager.instance.messagePanel.SetActive(true);
    }

    public override void SetInfo()
    {
        HUDManager.instance.messageText.text = "Press \"Pick Up\" button to pick up " + item.name;
    }

    public override void CloseInfo()
    {
        HUDManager.instance.messagePanel.SetActive(false);
    }

    public override void Interact()
    {
        PickUp();
    }

    public void PickUp()
    {
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
