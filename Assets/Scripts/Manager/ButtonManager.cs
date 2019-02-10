﻿using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    PlayerController playerController;

    void Start()
    {
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
    }

    public void OnEquipmentRemoveButton()
    {
        EquipmentManager.instance.Unequip();
    }

    public void OnInventoryButton()
    {
        playerController.ToggleInventoryPanel();
    }

    public void OnPickupButton()
    {
        playerController.Pickup();
    }

    public void OnActButton()
    {
        playerController.Act();
    }
}
