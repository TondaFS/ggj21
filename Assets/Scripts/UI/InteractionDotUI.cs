using UnityEngine;

public class InteractionDotUI : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private GameObject highlight;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.interactableObject != null)
            highlight.SetActive(true);
        else
            highlight.SetActive(false);
    }
}
