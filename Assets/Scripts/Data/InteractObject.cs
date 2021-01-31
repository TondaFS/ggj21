using UnityEngine;

[System.Serializable]
public class Interaction
{
#if UNITY_EDITOR
    [SerializeField] private string desc;
#endif
    public Property[] interactsWith;
    public InteractAction interactAction;
}

[CreateAssetMenu]
public class InteractObject : ScriptableObject
{
    public Interaction[] interactions;
    public AudioClip clip;
}
