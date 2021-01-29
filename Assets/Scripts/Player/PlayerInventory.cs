using UnityEngine;

public enum Hand { Right, Left }

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Item leftHandItem;
    [SerializeField] private Item rightHandItem;

    public bool AddItem(Hand hand, Item item)
    {
        if (hand == Hand.Right)
        {
            if (rightHandItem != null)
                return false;

            rightHandItem = item;
        }
        else
        {
            if (leftHandItem != null)
                return false;

            leftHandItem = item;
        }

        return true;
    }

    public Item DropItem(Hand hand)
    {
        if (hand == Hand.Right)
        {
            Item it = rightHandItem;
            rightHandItem = null;
            return it;
        }
        else
        {
            Item it = leftHandItem;
            leftHandItem = null;
            return it;
        }
    }

    public Item GetItem(Hand hand)
    {
        return hand == Hand.Left ? leftHandItem : rightHandItem;
    }
}
