using UnityEngine;

public enum ItemSize { Small, Medium, Large}

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public ItemSize size;
    public Sprite itemSprite;
    public AudioClip interactionSound;
    public Property[] properties;
    public bool isUnique;
}
