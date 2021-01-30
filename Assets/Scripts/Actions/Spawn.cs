using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Spawn")]
public class Spawn : InteractAction
{
    public Transform prefab;
    public override void Perform(InteractableObject interactableObject, Transform player)
    {
        InteractableWorldObject iwo = interactableObject as InteractableWorldObject;

        if (iwo == null)
            return;

        if (prefab == null)
        {
            Debug.LogWarning("spawn object missing! Doing nothing.");
            return;
        }

        Transform newObject = Instantiate(prefab);
        newObject.transform.position = iwo.transform.position + Vector3.up * 2;
    }
}
