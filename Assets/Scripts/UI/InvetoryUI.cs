using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvetoryUI : MonoBehaviour
{
    public PlayerInventory inventory;
    public TextMeshProUGUI leftHand;
    public TextMeshProUGUI rightHand;

    private void Awake()
    {
        inventory = FindObjectOfType<PlayerInventory>();
    }

    private void Update()
    {
        if (inventory.leftHandItem != null)
            leftHand.text = inventory.leftHandItem.name;
        else
            leftHand.text = "";

        if (inventory.rightHandItem != null)
            rightHand.text = inventory.rightHandItem.name;
        else
            rightHand.text = "";
    }
}
