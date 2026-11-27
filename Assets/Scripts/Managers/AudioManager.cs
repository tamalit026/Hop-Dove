using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TP2 - Eileen Moscovich.
public class AudioManager : Singleton<AudioManager>
{
    [System.Serializable]
    private class SoundEffectEntry
    {
        public SoundEffect effect;
        public AudioClip clip;
    }

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip levelMusic;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private SoundEffectEntry[] soundEffects;

    private Dictionary<SoundEffect, AudioClip> sfxDictionary =
        new Dictionary<SoundEffect, AudioClip>();

    protected override void Awake()
    {
        base.Awake();

        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();

        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.playOnAwake = false;

        sfxSource.playOnAwake = false;

        InitializeDictionary();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        PlayMusicForCurrentScene();
    }
    private void InitializeDictionary()
    {
        sfxDictionary.Clear();

        foreach (SoundEffectEntry entry in soundEffects)
        {
            if (entry.clip != null)
                sfxDictionary[entry.effect] = entry.clip;
        }
    }
    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }
    public void PlayLevelMusic()
    {
        PlayMusic(levelMusic);
    }
    private void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null)
            return;

        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(SoundEffect effect)
    {
        if (sfxDictionary.TryGetValue(effect, out AudioClip clip))
        {
            if (clip != null && sfxSource != null)
                sfxSource.PlayOneShot(clip);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene();
    }
    private void PlayMusicForCurrentScene()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            PlayMenuMusic();
        }
        else
        {
            PlayLevelMusic();
        }
    }
    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }
}
