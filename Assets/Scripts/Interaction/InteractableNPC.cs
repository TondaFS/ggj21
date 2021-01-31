using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

public enum NPCState { Quest, WaitForItem, Cooldown, AfterCooldown, ItemGained}

public class InteractableNPC : InteractableObject
{
    public NPC data;

    public NPCState currentState;

    private Coroutine itemCooldown;
    private Coroutine dialogueCoroutine;
    private TextMeshPro meshPro;

    private PlayerInventory inventory;
    private bool gotItem = false;

    public GameObject background;

    private void Awake()
    {
        currentState = NPCState.Quest;
        meshPro = GetComponentInChildren<TextMeshPro>();
        meshPro.text = "";
        inventory = FindObjectOfType<PlayerInventory>();
        gotItem = false;
        background.SetActive(false);
    }

    public override void Interact(Hand hand, Item item)
    {
        if (dialogueCoroutine != null)
            StopCoroutine(dialogueCoroutine);

        if (gotItem)
        {
            ShowText(data.goodItem);
            return;
        }

        if (item == null)
        {
            switch (currentState) 
            {
                case NPCState.Quest:
                    ShowText(data.questStart);
                    break;
                case NPCState.WaitForItem:
                    ShowText(data.questStart);
                    break;
                case NPCState.Cooldown:
                    ShowText(data.okItem);
                    break;
                case NPCState.AfterCooldown:
                    ShowText(data.lostItem);
                    break;
                case NPCState.ItemGained:
                    ShowText(data.goodItem);
                    break;
            }            
        }
        else
        {
            if (item == data.neededItem)
            {
                ShowText(data.goodItem);
                currentState = NPCState.ItemGained;
                Storm.Instance.RemoveHealth(InventoryItemSpawner.Instance.goodItemStormValue);
                inventory.RemoveItem(hand);
                gotItem = true;
                return;
            }

            if (itemCooldown != null)
            {
                ShowText(data.okItem);                
                return;
            }
                
            if (IsOkItem(item))
            {
                ShowText(data.okItem);
                currentState = NPCState.Cooldown;
                StartCooldown();
                Storm.Instance.RemoveHealth(InventoryItemSpawner.Instance.okItemStromValue);
                inventory.RemoveItem(hand);
            }
            else
            {
                ShowText(data.wrongItem);
            }
        }
    }

    private void StartCooldown()
    {
        itemCooldown = StartCoroutine(ToLostCooldown());
    }

    private IEnumerator ToLostCooldown()
    {
        yield return new WaitForSeconds(data.okItemCooldown);
        currentState = NPCState.AfterCooldown;
        itemCooldown = null;
    }

    private bool IsOkItem(Item item)
    {
        if (item == null)
            return false;

        return item.properties.Contains(data.okProperty);
    }

    private void ShowText(string text)
    {
        if (dialogueCoroutine != null)
            StopCoroutine(dialogueCoroutine);

        background.SetActive(true);
        meshPro.text = text;
        dialogueCoroutine = StartCoroutine(DialogueCooldown());
    }

    private IEnumerator DialogueCooldown()
    {
        yield return new WaitForSeconds(10);
        meshPro.text = "";
        background.SetActive(false);
    }

    private void LateUpdate()
    {
        var lookPos = PlayerInput.Instance.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;// Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
        transform.Rotate(0, 180, 0);

       // transform.LookAt(PlayerInput.Instance.transform);
        //meshPro.transform.Rotate(0, 180, 0);
    }
}
