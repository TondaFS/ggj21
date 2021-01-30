using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Push")]
public class Push : InteractAction
{
    public float pushMultiplier = 2f;

    public override void Perform(InteractableObject interactableObject, Transform player)
    {
        Vector3 pushVector = interactableObject.transform.position - player.transform.position;
        pushVector = pushVector.normalized;

        Rigidbody rigidb = interactableObject.GetComponent<Rigidbody>();
        rigidb.AddForce(pushVector * pushMultiplier, ForceMode.Impulse);
        Debug.Log("Pushed");
    }
}
