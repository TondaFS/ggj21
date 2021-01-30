using System;

public class InteractableInvetoryItem : InteractableObject
{
    public static event Action<Hand, Item> ItemPicked;
    public Item item;

    public override void Interact(Hand hand, Item item)
    {
        //check if player has item in his hand already
        if (item != null)
            return;

        ItemPicked?.Invoke(hand, this.item);
        Destroy(this.gameObject);
    }
}
