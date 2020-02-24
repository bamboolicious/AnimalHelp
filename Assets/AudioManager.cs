using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [HideInInspector] public AudioSource audioSource;
    public AudioClip playerJump, playerHit, playerWalk, playerIncorrect, playerCorrect, onGameEnd, buttonPressed;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlayWalk()
    {
        audioSource.PlayOneShot(playerWalk);

    }
    public void PlayJump()
    {
        audioSource.PlayOneShot(playerJump);
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(playerHit);
    }

    public void PlayIncorrect()
    {
        audioSource.PlayOneShot(playerIncorrect);
    }
    public void PlayCorrect()
    {
        audioSource.PlayOneShot(playerCorrect);
    }
    public void PlayGameEnd()
    {
        audioSource.PlayOneShot(onGameEnd);
    }
    public void PlayButtonPress()
    {
        audioSource.PlayOneShot(buttonPressed);
    }

}