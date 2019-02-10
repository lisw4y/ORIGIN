using UnityEngine;

public enum InteractableType
{
    DroppedItem,
    Resource,
    Enemy
}

public abstract class Interactable : MonoBehaviour
{
    public InteractableType interactableType;

    public abstract void ShowInfo();

    public abstract void SetInfo();

    public abstract void CloseInfo();

    public abstract void Interact();
}
