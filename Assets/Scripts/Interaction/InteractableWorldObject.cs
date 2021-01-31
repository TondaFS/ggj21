using System.Linq;
using UnityEngine;

public class InteractableWorldObject : InteractableObject
{
    public InteractObject interactObject;
    public GameObject replacement;

    private void LateUpdate()
    {
        Transform parent = transform.parent;
        transform.SetParent(null);
        parent.transform.position = transform.position;
        parent.transform.rotation = transform.rotation;
        transform.SetParent(parent);
    }

    public override void Interact(Hand hand, Item item)
    {
        Debug.Log("Interact with: " + item);
        foreach (Interaction interaction in interactObject.interactions)
        {
            if (IsInteractableWithItem(interaction, item))
            {
                Debug.Log("Its interactable: " + this.gameObject);
                
                AudioManager.Instance.PlaySound(interactObject.clip);
                interaction.interactAction.Perform(this, PlayerInput.Instance.transform);
                return;
            }
            else
                Debug.Log("Not interactable " + this.gameObject);
        }        
    }      
    
    private bool IsInteractableWithItem(Interaction interaction, Item item)
    {
        if (item == null)
            return false;

        Debug.Log("Properties: " + item.properties.Length);
        foreach (Property p in item.properties)
        {
            Debug.Log("Contains? " + p);
            if (interaction.interactsWith.Contains(p))
                return true;
        }

        return false;
    }
}
