using UnityEngine;

[CreateAssetMenu]
public class NPC : ScriptableObject
{
    public string questStart;
    public string wrongItem;
    public string okItem;
    public string goodItem;
    public string lostItem;
    public AudioClip clip;

    [Space(15)]
    public Item neededItem;
    public Property okProperty;
    public float okItemCooldown = 10;
}
