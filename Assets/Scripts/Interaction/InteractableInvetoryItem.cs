using System;
using System.Collections;
using UnityEngine;

public class InteractableInvetoryItem : InteractableObject
{
    public static event Action<Hand, Item> ItemPicked;
    public Item item;

    private SpriteRenderer spriteRenderer;
    public ItemSpawn spawn;
    public bool canBeDestroyed = false;

    public override void Interact(Hand hand, Item item)
    {
        //check if player has item in his hand already
        if (item != null)
            return;

        ItemPicked?.Invoke(hand, this.item);

        if (spawn != null)
        {
            spawn.hasItem = false;

            if (spawn.isInStorm && !spawn.isUnique)
                InventoryItemSpawner.Instance.AddSpawnToStorm(spawn);
        }

        Destroy(this.gameObject);
    }

    private void LateUpdate()
    {
        transform.LookAt(PlayerInput.Instance.transform);
        transform.Rotate(0, 180, 0);
    }

    private void Awake()
    {
        canBeDestroyed = false;
        if (item == null)
            return;

        SetCorrectSprite();
        StartCoroutine(WaitForEnable());
    }

    private IEnumerator WaitForEnable()
    {
        yield return new WaitForSeconds(1);
        canBeDestroyed = true;
    }

    public void SetCorrectSprite()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.itemSprite;
    }
}
