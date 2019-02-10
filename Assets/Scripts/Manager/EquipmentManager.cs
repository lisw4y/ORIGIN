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
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
    }

    public void Equip(Equipment newItem)
    {
        Equipment oldItem = null;

        if (CurrentEquipment != null)
        {
            oldItem = CurrentEquipment;
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem);

        CurrentEquipment = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void Unequip()
    {
        if (CurrentEquipment != null)
        {
            inventory.Add(CurrentEquipment);

            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null);

            CurrentEquipment = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
        }
    }
}
