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
    public SkinnedMeshRenderer mesh;
    public Material[] materials;
    public int[] amountNeeded;

    private void Awake()
    {
        setCount(1);
    }
    public override void Use()
    { 
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}
