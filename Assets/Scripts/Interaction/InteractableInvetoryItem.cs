using System;
using UnityEngine;

public class InteractableInvetoryItem : InteractableObject
{
    public static event Action<Hand, Item> ItemPicked;
    public Item item;

    private SpriteRenderer spriteRenderer;

    public override void Interact(Hand hand, Item item)
    {
        //check if player has item in his hand already
        if (item != null)
            return;

        ItemPicked?.Invoke(hand, this.item);
        Destroy(this.gameObject);
    }

    private void LateUpdate()
    {
        transform.LookAt(PlayerInput.Instance.transform);
        transform.Rotate(0, 180, 0);
    }

    private void Awake()
    {
        if (item == null)
            return;

        SetCorrectSprite();
    }

    public void SetCorrectSprite()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.itemSprite;
    }
}
