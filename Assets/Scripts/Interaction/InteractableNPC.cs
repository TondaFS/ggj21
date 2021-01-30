using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum NPCState { Quest, WaitForItem, Cooldown, AfterCooldown, ItemGained}

public class InteractableNPC : InteractableObject
{
    public NPC data;

    public NPCState currentState;

    private Coroutine itemCooldown;
    private Coroutine dialogueCoroutine;
    private TextMeshPro meshPro;

    public override void Interact(Hand hand, Item item)
    {
        if (dialogueCoroutine != null)
            StopCoroutine(dialogueCoroutine);

        meshPro.text = data.questStart;
    }

    private IEnumerator DialogueCooldown()
    {
        yield return new WaitForSeconds(10);        
    }

    private void Awake()
    {
        currentState = NPCState.Quest;
        meshPro = GetComponentInChildren<TextMeshPro>();
        meshPro.text = "";
    }

    private void LateUpdate()
    {
        meshPro.transform.LookAt(PlayerInput.Instance.transform);
        meshPro.transform.Rotate(0, 180, 0);
    }
}
