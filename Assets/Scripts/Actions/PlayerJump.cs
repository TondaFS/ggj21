using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[CreateAssetMenu(menuName = "Actions/Jump")]
public class PlayerJump : InteractAction
{
    public float amount = 5;
    public override void Perform(InteractableObject interactableObject, Transform player)
    {
        Rigidbody rigidb = player.GetComponent<Rigidbody>();

        rigidb.AddForce(Vector3.up * amount, ForceMode.Impulse);
        Debug.Log("JUMP");
        FirstPersonController controller = player.GetComponent<FirstPersonController>();
        controller.ChangeJump();           
    }
}
