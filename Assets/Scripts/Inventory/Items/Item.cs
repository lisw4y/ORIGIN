using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    [HideInInspector] public bool isStackable = false;

    public virtual void Use(int index)
    {

    }

    public void RemoveFromInventory(int index)
    {
        Inventory.instance.Remove(index);
    }
}
