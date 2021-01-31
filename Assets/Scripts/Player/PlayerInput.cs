using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    private PlayerInventory inventory;
    public InteractableObject interactableObject;

    [SerializeField] private float interactDistance = 3;
    [SerializeField] private float interactCheckDelay = 0.3f;
    private float lastTimeCheck;    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            return;

        inventory = GetComponent<PlayerInventory>();
        lastTimeCheck = Time.time;
    }

    private void Update()
    {
        CheckInteractableObject();

        HandleLeftMouseButton();
        HandleRightMouseButton();
        HandleDropButtons();
    }

    private void CheckInteractableObject()
    {
        if (Time.time - lastTimeCheck <= interactCheckDelay)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Physics.Raycast(ray, out hit, interactDistance, Layers.Mask.DetectInteractableObject ))
        {
            //Debug.Log("HIT: " + hit.transform);
            interactableObject = hit.collider.gameObject.GetComponent<InteractableObject>();
            // Do something with the object that was hit by the raycast.
        }
        else
        {
            interactableObject = null;
        }
    }

    private void HandleDropButtons()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            DropItem(Hand.Left);
        else if (Input.GetKeyDown(KeyCode.E))
            DropItem(Hand.Right);
    }

    private void DropItem(Hand hand)
    {
        Item item = inventory.DropItem(hand);

        if (item == null)
            return;

        InteractableInvetoryItem newItem = InventoryItemSpawner.Instance.CreateItem(item, (transform.position + transform.forward + Vector3.down * 0.75f), null);       
    }

    private void HandleLeftMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InvetoryUI.Instance.PlayeAnimation(Hand.Left);

            if (inventory.leftHandItem == null)
                AudioManager.Instance.PlaySlap();
            else
                AudioManager.Instance.PlaySound(inventory.leftHandItem.interactionSound);

            Debug.Log("Left Mouse Button!");
            if (interactableObject != null)
            {
                Debug.Log("Interact with object");
                interactableObject.Interact(Hand.Left, inventory.GetItem(Hand.Left));
            }
        }
    }
    private void HandleRightMouseButton()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            if (inventory.rightHandItem == null)
                AudioManager.Instance.PlaySlap();
            else
                AudioManager.Instance.PlaySound(inventory.rightHandItem.interactionSound);

            InvetoryUI.Instance.PlayeAnimation(Hand.Right);
            Debug.Log("Right Mouse Button!");
            if (interactableObject != null)
            {
                Debug.Log("Interact with object");
                interactableObject.Interact(Hand.Right, inventory.GetItem(Hand.Right));
            }
        }
    }

}
