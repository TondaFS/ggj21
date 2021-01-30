using UnityEngine;

public abstract class InteractAction : ScriptableObject
{
    //jeste pridat Hrace, protoze ho budeme chtit nechat skakat nebo tak
    public abstract void Perform(InteractableObject interactableObject);
}
