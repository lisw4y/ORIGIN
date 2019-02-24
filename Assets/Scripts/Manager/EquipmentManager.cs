using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnEquipmentChanged(Item item);
    public OnEquipmentChanged onEquipmentChanged;
    public Image icon;
    public Button removeButton;

    public Equipment CurrentEquipment { get; private set; }
    GameObject instantiatedItem;
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        print("EquipmentManager:inventory: " + inventory);
    }

    public void Equip(Equipment newItem)
    {
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem);

        Unequip();
        CurrentEquipment = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        instantiatedItem = Instantiate(newItem.instantiatedItem);
        instantiatedItem.transform.parent = PlayerManager.instance.player.GetComponent<PlayerController>().hand.transform;
        instantiatedItem.transform.localPosition = newItem.EquipPosition;
        instantiatedItem.transform.localEulerAngles = newItem.EquipRotation;
    }

    public void Unequip()
    {
        if (CurrentEquipment != null)
        {
            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null);

            CurrentEquipment = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;

            Destroy(instantiatedItem);
        }
    }
}
