using UnityEngine;

[CreateAssetMenu( menuName = "Actions/Destroy")]
public class Destroy : InteractAction
{
    public ParticleSystem particle;

    public override void Perform(InteractableObject interactableObject, Transform player)
    {
        if (particle != null)
        {
            ParticleSystem particleSys = Instantiate(particle);
            particleSys.transform.position = interactableObject.transform.position;
        }

        Destroy(interactableObject.transform.parent.gameObject);
    }
}
