using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    public GameObject endGameScreen;

    private void OnEnable()
    {
        Storm.StormFinished += OnStormEnded;
    }
    private void OnDisable()
    {
        Storm.StormFinished -= OnStormEnded;

    }

    private void OnStormEnded()
    {
        
        endGameScreen.SetActive(true); 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
