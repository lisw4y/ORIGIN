using UnityEngine;

public enum EquipmentType
{
    Tool
}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType equipmentType;
    public int attackPower;
    public _Material[] materials;
    public int[] amountNeeded;
    public GameObject instantiatedItem;
    public Vector3 EquipPosition;
    public Vector3 EquipRotation;

    public override void Use(int index)
    {
        base.Use(index);
        EquipmentManager.instance.Equip(this);
    }
}
