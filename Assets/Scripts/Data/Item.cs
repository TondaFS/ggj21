using UnityEngine;

public enum ItemSize { Small, Normal, Big}

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public ItemSize size;
    public Sprite itemSprite;
    public Property[] properties;
}
