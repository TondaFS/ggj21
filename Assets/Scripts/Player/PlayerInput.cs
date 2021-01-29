using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInventory inventory;
    private InteractableObject interactableObject;

    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        HandleLeftMouseButton();
        HandleRightMouseButton();
    }

    private void HandleLeftMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (interactableObject != null)
            {
                interactableObject.Interact(inventory.GetItem(Hand.Left));
            }
        }
    }
    private void HandleRightMouseButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (interactableObject != null)
            {
                interactableObject.Interact(inventory.GetItem(Hand.Right));
            }
        }
    }
    
}
