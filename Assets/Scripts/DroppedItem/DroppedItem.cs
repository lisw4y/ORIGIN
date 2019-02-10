using UnityEngine;
using UnityEngine.UI;

public class DroppedItem : Interactable
{
    public Item item;
    public GameObject messagePanel;

    Text messageText;
    
    void Start()
    {
        interactableType = InteractableType.DroppedItem;
        messageText = messagePanel.GetComponentInChildren<Text>();
    }

    public override void ShowInfo()
    {
        SetInfo();
        messagePanel.SetActive(true);
    }

    public override void SetInfo()
    {
        messageText.text = "Press \"Pick Up\" button to pick up " + item.name;
    }

    public override void CloseInfo()
    {
        messagePanel.SetActive(false);
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
