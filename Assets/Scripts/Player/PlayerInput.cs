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

    private void HandleLeftMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
            Debug.Log("Right Mouse Button!");
            if (interactableObject != null)
            {
                Debug.Log("Interact with object");
                interactableObject.Interact(Hand.Right, inventory.GetItem(Hand.Right));
            }
        }
    }
    
}
