using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "Inventory/Material")]
public class Material : Item
{
    void Awake()
    {
        setCount(1);
        isStackable = true;
    }
}
