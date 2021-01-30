using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Replace")]
public class Replace : InteractAction
{
    public override void Perform(InteractableObject interactableObject, Transform player)
    {
        InteractableWorldObject iwo = interactableObject as InteractableWorldObject;

        if (iwo == null)
            return;

        if (iwo.replacement == null)
        {
            Debug.LogWarning("replacement object missing! Doing nothing.");
            return;
        }

        iwo.replacement.SetActive(true);        
        Destroy(iwo.transform.root.gameObject);
    }
}
