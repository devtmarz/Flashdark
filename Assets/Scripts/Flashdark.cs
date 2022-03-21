using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashdark : MonoBehaviour
{

    WorldState worldState;
    bool _canUse;
    public float rechargeSeconds = 5f;

    public AudioSource flashdarkAudioSource;
    
    public AudioClip readyToUseSound;
    public AudioClip denyInputSound;
    public AudioClip depletedSound;

    public Material flashdarkLights;
    Color readyToUseColor = new Color(0.05490196f, 1.403922f, 0f);
    private void Start()
    {
        worldState = FindObjectOfType<WorldState>();
    }

    private void OnEnable()
    {
        _canUse = true;
        flashdarkLights.SetColor("_EmissionColor", readyToUseColor);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        if (worldState != null)
            worldState.SetWorldDark(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_canUse)
            {
                StartCoroutine("UsingFlashdark");
            }
            else
            {
                PlaySoundClip(denyInputSound);
            }
        }
    }
    IEnumerator UsingFlashdark()
    {
        _canUse = false;
        worldState.SetWorldDark(true);
        yield return new WaitForSecondsRealtime(2f);
        PlaySoundClip(depletedSound);
        yield return new WaitForSecondsRealtime(0.7f);
        flashdarkLights.SetColor("_EmissionColor", Color.red);
        worldState.SetWorldDark(false);
        yield return new WaitForSecondsRealtime(rechargeSeconds);
        flashdarkLights.SetColor("_EmissionColor", readyToUseColor);
        PlaySoundClip(readyToUseSound);
        _canUse = true;
    }

    void PlaySoundClip(AudioClip audioClip)
    {
        if (flashdarkAudioSource.isPlaying)
        {
            if (audioClip == denyInputSound)
            {
                return;
            }
            flashdarkAudioSource.Stop();
        }
        flashdarkAudioSource.clip = audioClip;
        flashdarkAudioSource.Play();
    }
}