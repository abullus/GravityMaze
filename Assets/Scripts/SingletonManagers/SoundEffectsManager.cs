using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public AudioClip playerCollisionSound;
    public AudioClip finishSound;
    public AudioClip changeGravity;

    public GameObject mainCamera;

    private bool soundOn;

    public static SoundEffectsManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsManager!");
        }
        Instance = this;
    }
    
    public void Start()
    {
        CheckSettings();
    }

    public void CheckSettings()
    {
        soundOn = SettingsManager.Instance.SoundOn;
    }

    public void MakeCollisionSound(float volume)
    {
        MakeSound(playerCollisionSound, volume);
    }

    public void ChangeGravity()
    {
        //MakeSound(changeGravity);
    }
    
    public void FinishSound()
    {
        //MakeSound(finishSound);
    }

    private void MakeSound(AudioClip audioClip, float volume = 1)
    {
        if (!soundOn) return;
        AudioSource.PlayClipAtPoint(audioClip, mainCamera.transform.position, volume);
    }
}
