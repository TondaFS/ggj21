using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public abstract void Interact(Hand hand, Item item);
}
