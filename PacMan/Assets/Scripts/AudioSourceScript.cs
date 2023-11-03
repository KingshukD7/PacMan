using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceScript : MonoBehaviour
{
    public AudioClip introMusic;
    public AudioClip ghostBackgroundMusic;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayIntro();
    }

    void PlayIntro()
    {
        audioSource.clip = introMusic;
        audioSource.Play();
        Invoke("PlayGhostBackgroundMusic", introMusic.length); 
    }

    void PlayGhostBackgroundMusic()
    {
        audioSource.clip = ghostBackgroundMusic;
        audioSource.Play();
    }
}
