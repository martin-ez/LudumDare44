﻿using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Range(0, 1)]
    public float SfxVolume;
    [Range(0, 1)]
    public float MusicVolume;

    AudioSource sfx;
    AudioSource dragonSound;
    AudioSource[] musicSources;

    public enum FXSound
    {
        Gain,
        Lost,
        Bounce,
        Fire_Shot,
    }

    public enum DragonSound
    {
        Growl,
        Flap,
    }

    [Header("Fx Clips")]
    public AudioClip gain;
    public AudioClip lost;
    public AudioClip bounce;
    public AudioClip fireshot;

    public AudioClip[] growl;
    public AudioClip[] flap;

    [Header("Song Clips")]
    public AudioClip song1;
    public AudioClip song2;
    private int current;

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;

            GameObject sfx2DS = new GameObject("SFX_Source");
            sfx2DS.transform.SetParent(transform);
            sfx = sfx2DS.AddComponent<AudioSource>();
            sfx.volume = SfxVolume;

            GameObject dragon2DS = new GameObject("Dragon_Source");
            dragon2DS.transform.SetParent(transform);
            dragonSound = dragon2DS.AddComponent<AudioSource>();
            dragonSound.volume = SfxVolume;

            musicSources = new AudioSource[2];

            GameObject music1Go = new GameObject("MusicSource1");
            music1Go.transform.SetParent(transform);
            musicSources[0] = music1Go.AddComponent<AudioSource>();
            musicSources[0].clip = song1;
            musicSources[0].volume = 0;

            GameObject music2Go = new GameObject("MusicSource2");
            music2Go.transform.SetParent(transform);
            musicSources[1] = music2Go.AddComponent<AudioSource>();
            musicSources[1].clip = song2;
            musicSources[1].volume = 0;

            current = 0;
            StartCoroutine(FadeIn());
        }
    }

    public void ChangeSong()
    {
        StartCoroutine(FadeSong());
    }

    public void PlayFxSound(FXSound clipName)
    {
        AudioClip clip = null;
        switch (clipName)
        {
            case FXSound.Gain:
                clip = gain;
                break;
            case FXSound.Lost:
                clip = lost;
                break;
            case FXSound.Bounce:
                clip = bounce;
                break;
            case FXSound.Fire_Shot:
                clip = fireshot;
                break;
        }
        if (clip != null)
        {
            sfx.clip = clip;
            sfx.time = 0f;
            sfx.loop = false;
            sfx.Play();
        }
    }

    public void PlayDragonSound(DragonSound clipName, int level)
    {
        AudioClip clip = null;
        switch (clipName)
        {
            case DragonSound.Growl:
                clip = growl[level];
                break;
            case DragonSound.Flap:
                clip = flap[level];
                break;
        }
        if (clip != null)
        {
            dragonSound.clip = clip;
            dragonSound.time = 0f;
            dragonSound.loop = false;
            dragonSound.Play();
        }
    }

    IEnumerator FadeSong()
    {
        float time = 0;
        float i = 0;
        while (i < 1f)
        {
            time += Time.deltaTime;
            i = time / 1f;

            musicSources[current].volume = Mathf.Lerp(MusicVolume, 0f, i);
            musicSources[Mathf.Abs(current - 1)].volume = Mathf.Lerp(0f, MusicVolume, i);
            yield return null;
        }
        musicSources[current].volume = 0f;
        musicSources[Mathf.Abs(current - 1)].volume = MusicVolume;
        current = Mathf.Abs(current - 1);
    }

    IEnumerator FadeIn()
    {
        float time = 0;
        float i = 0;
        while (i < 1f)
        {
            time += Time.deltaTime;
            i = time / 1f;

            musicSources[current].volume = Mathf.Lerp(0f, MusicVolume, i);
            yield return null;
        }
        musicSources[current].volume = MusicVolume;
    }
}