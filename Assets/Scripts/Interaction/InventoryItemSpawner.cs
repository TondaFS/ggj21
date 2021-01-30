using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSpawner : MonoBehaviour
{
    public static InventoryItemSpawner Instance;
    public InteractableInvetoryItem inventoryItemPrefab;

    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    public InteractableInvetoryItem CreateItem(Item item, Vector3 position)
    {
        InteractableInvetoryItem newItem = Instantiate(inventoryItemPrefab, position, Quaternion.identity);
        newItem.item = item;

        //add image change etc...
        newItem.name = "item_" + item.name;
        return newItem;
    }
}
