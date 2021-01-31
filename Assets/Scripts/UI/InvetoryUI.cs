using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvetoryUI : MonoBehaviour
{
    public static InvetoryUI Instance; 

    public PlayerInventory inventory;
    public Image leftHand;
    public Image rightHand;

    public Animator lefthandAnimation;
    public Animator rightHandAnimation;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        inventory = FindObjectOfType<PlayerInventory>();
    }

    private void Update()
    {
        if (inventory.leftHandItem != null)
        {
            leftHand.sprite = inventory.leftHandItem.itemSprite;
            leftHand.gameObject.SetActive(true);
        }            
        else
            leftHand.gameObject.SetActive(false);

        if (inventory.rightHandItem != null)
        {
            rightHand.sprite = inventory.rightHandItem.itemSprite;
            rightHand.gameObject.SetActive(true);
        }
        else
            rightHand.gameObject.SetActive(false);
    }

    public void PlayeAnimation(Hand hand)
    {
        if (hand == Hand.Left)
            lefthandAnimation.SetTrigger("Animation");
        else
            rightHandAnimation.SetTrigger("Animation");
    }
}
