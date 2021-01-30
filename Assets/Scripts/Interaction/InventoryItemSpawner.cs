using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSpawner : MonoBehaviour
{
    public static InventoryItemSpawner Instance;

    public InteractableInvetoryItem inventorySmallItemPrefab;
    public InteractableInvetoryItem inventoryMediumItemPrefab;
    public InteractableInvetoryItem inventoryLargeItemPrefab;

    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    public InteractableInvetoryItem CreateItem(Item item, Vector3 position)
    {
        InteractableInvetoryItem prefab = item.size == ItemSize.Small ? inventorySmallItemPrefab :
            item.size == ItemSize.Medium ? inventoryMediumItemPrefab : inventoryLargeItemPrefab;

        InteractableInvetoryItem newItem = Instantiate(prefab, position, Quaternion.identity);
        newItem.item = item;

        newItem.SetCorrectSprite();
        newItem.name = "item_" + item.name;
        return newItem;
    }
}
