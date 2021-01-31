using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip[] slaps;

    public AudioSource audioSource;
       

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySlap()
    {
        int slap = Random.Range(0, slaps.Length);
        audioSource.PlayOneShot(slaps[slap]);
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }


}
