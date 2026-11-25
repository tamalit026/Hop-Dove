using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip[] soundEffects;
    [SerializeField] private AudioSource audioSource;

    private Dictionary<SoundEffect, AudioClip> sfxDictionary = new Dictionary<SoundEffect, AudioClip>();

    protected override void Awake()
    {
        base.Awake();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        for (int i = 0; i < soundEffects.Length; i++)
        {
            if (Enum.IsDefined(typeof(SoundEffect), i))
            {
                sfxDictionary[(SoundEffect)i] = soundEffects[i];
            }
        }
    }

    public void PlayMusic()
    {
        if (music != null && audioSource != null)
        {
            audioSource.clip = music;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlaySFX(SoundEffect effect)
    {
        if (sfxDictionary.TryGetValue(effect, out AudioClip clip) && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying) audioSource.Stop();
    }
}
