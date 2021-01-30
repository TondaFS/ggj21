using System.Linq;
using UnityEngine;

public class InteractableWorldObject : InteractableObject
{
    public InteractObject interactObject;
    public override void Interact(Hand hand, Item item)
    {
        Debug.Log("Interact with: " + item);
        foreach (Interaction interaction in interactObject.interactions)
        {
            if (IsInteractableWithItem(interaction, item))
            {
                Debug.Log("Its interactable: " + this.gameObject);
                interaction.interactAction.Perform(this);
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
