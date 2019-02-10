using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isStackable = false;
    public int count = 1;

    public virtual void Use()
    {

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
