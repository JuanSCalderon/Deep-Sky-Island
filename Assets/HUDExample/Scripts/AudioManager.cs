using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sFxSounds;
    public AudioSource musicSource, sfxSource;


    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("GamePlayMusic");
    }

    public void PlayMusic(string name)
    {

        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void StopAndPlayMusic(string name)
    {
        musicSource.Stop();
        PlayMusic(name);
    }

    public void PlaySFX(string name, float volume = 1f, float pitch = 1f)
    {

        Sound s = Array.Find(sFxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.volume = volume;
            sfxSource.pitch = pitch;

            sfxSource.PlayOneShot(s.clip);
        }
    }

    public AudioSource PlaySFXLoop(string name, float volume = 1f, float pitch = 1f)
    {
        Sound s = Array.Find(sFxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
            return null;
        }
        else
        {
            AudioSource loopSource = gameObject.AddComponent<AudioSource>();
            loopSource.clip = s.clip;
            loopSource.volume = volume;
            loopSource.pitch = pitch;
            loopSource.loop = true;
            loopSource.Play();
            return loopSource;
        }
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }
}
