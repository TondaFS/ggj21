using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UniqueSpawn
{
#if UNITY_EDITOR
    [SerializeField] private string desc;
#endif
    public ItemSpawn[] spawn;
    public Item item;
}

public class InventoryItemSpawner : MonoBehaviour
{
    public static InventoryItemSpawner Instance;

    public InteractableInvetoryItem inventorySmallItemPrefab;
    public InteractableInvetoryItem inventoryMediumItemPrefab;
    public InteractableInvetoryItem inventoryLargeItemPrefab;

    public UniqueSpawn[] uniqueSpawns;
    public List<ItemSpawn> inStormSpawns;
    public ItemSpawn[] allSpawns;

    public Item[] allItems;
    private Queue<Item> itemQueue;
    public int duplicateCount = 3;
    public float waitForNewSpawn = 5;

    [Header("Item values")]
    public int goodItemStormValue = 15;
    public int okItemStromValue = 5;
        
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        if (duplicateCount <= 1)
            duplicateCount = 2;

        inStormSpawns = new List<ItemSpawn>();
        PrepareQueue();
        SpawnItems();
        SpawnUniques();
    }

    private void SpawnUniques()
    {
        foreach (UniqueSpawn item in uniqueSpawns)
        {
            UniqueSpawn uniqueSpawn = GetUniqueSpawn(item.item);
            int randomSpawn = Random.Range(0, uniqueSpawn.spawn.Length);
            ItemSpawn spawn = uniqueSpawn.spawn[randomSpawn];
            CreateItem(item.item, spawn.transform.position, spawn);
        }
    }

    private void SpawnItems()
    {
        System.Random rnd = new System.Random();
        rnd.Shuffle(allSpawns);

        int count = itemQueue.Count - allItems.Length;
        for (int i = 0; i < count; i++)
        {
            Item it = itemQueue.Dequeue();
            if (it == null)
                break;

            ItemSpawn spawn = allSpawns[i];
            if (spawn == null)
                break;

            CreateItem(it, spawn.transform.position, spawn);
        }
    }

    private UniqueSpawn GetUniqueSpawn(Item item)
    {
        foreach (UniqueSpawn spawn in uniqueSpawns)
        {
            if (spawn.item == item)
                return spawn;
        }

        return null;
    }

    private void PrepareQueue()
    {
        itemQueue = new Queue<Item>();
        System.Random rnd = new System.Random();
        
        for (int i = 0; i < duplicateCount; i++)
        {
            rnd.Shuffle(allItems);
            foreach (Item it in allItems)
            {
                itemQueue.Enqueue(it);
            }
        }
    }

    public void AddSpawnToStorm(ItemSpawn spawn)
    {
        Debug.Log("Trying to add spawn to storm");
        if (spawn.hasItem)
        {
            Debug.Log("It has item");
            return;
        }

        Debug.Log("Spawn added to storm Spawns");
        inStormSpawns.Add(spawn);
    }

    public void RemoveSpawnFromStorm(ItemSpawn spawn)
    {
        Debug.Log("Spawn removed from storm");
        inStormSpawns.Remove(spawn);
    }

    public void RemoveItemDueStorm(InteractableInvetoryItem item)
    {
        Debug.Log("Removing item due storm: " + item);

        if (item.item.isUnique)
        {
            if (item.spawn != null)
                item.spawn.hasItem = false;

            UniqueSpawn uniqueSpawn = GetUniqueSpawn(item.item);
            int randomSpawn = Random.Range(0, uniqueSpawn.spawn.Length);
            ItemSpawn spawn = uniqueSpawn.spawn[randomSpawn];
            CreateItem(item.item, spawn.transform.position, spawn);
            Destroy(item.gameObject);
            Debug.Log("New unique item created");
        }
        else
        {
            if (item.spawn != null)
            {
                item.spawn.hasItem = false;
                inStormSpawns.Add(item.spawn);
            }

            itemQueue.Enqueue(item.item);

            StartCoroutine(WaitForNewSpawn());
            Destroy(item.gameObject);
            Debug.Log("New Item due storm created");
        }
    }

    private IEnumerator WaitForNewSpawn()
    {
        yield return new WaitForSeconds(waitForNewSpawn);

        if (inStormSpawns.Count <= 0)
        {
            Debug.LogWarning("No spawns in storm! Doing nothing!");
            yield break;
        }

        int randomSpawn = Random.Range(0, inStormSpawns.Count);
        ItemSpawn newSpawn = inStormSpawns[randomSpawn];
        inStormSpawns.RemoveAt(randomSpawn);
        Item newItem = itemQueue.Dequeue();
        CreateItem(newItem, newSpawn.transform.position, newSpawn);
    }

    public InteractableInvetoryItem CreateItem(Item item, Vector3 position, ItemSpawn spawn)
    {
        Debug.Log("Creating new item: " + item.name);
        InteractableInvetoryItem prefab = item.size == ItemSize.Small ? inventorySmallItemPrefab :
            item.size == ItemSize.Medium ? inventoryMediumItemPrefab : inventoryLargeItemPrefab;

        InteractableInvetoryItem newItem = Instantiate(prefab, position, Quaternion.identity);
        newItem.item = item;
        newItem.spawn = spawn;

        if (spawn != null)
            spawn.hasItem = true;

        newItem.SetCorrectSprite();
        newItem.name = "item_" + item.name;
        return newItem;
    }
}
