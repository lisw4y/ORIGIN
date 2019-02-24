using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "Inventory/Material")]
public class _Material : Item
{
    void Awake()
    {
        isStackable = true;
    }
}
