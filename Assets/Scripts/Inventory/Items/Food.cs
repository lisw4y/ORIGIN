using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Item
{
    public int restoreFoodValue;
    public int restoreHealthValue;
    int reduceQuantity = 1;

    void Awake()
    {
        isStackable = true;
    }

    public override void Use(int index)
    {
        base.Use(index);
        Inventory.instance.ReduceStock(this, reduceQuantity);
        if (restoreFoodValue > 0)
            PlayerManager.instance.player.GetComponent<PlayerStats>().RestoreFood(restoreFoodValue);
        if (restoreHealthValue > 0)
            PlayerManager.instance.player.GetComponent<PlayerStats>().RestoreHealth(restoreHealthValue);
    }
}
