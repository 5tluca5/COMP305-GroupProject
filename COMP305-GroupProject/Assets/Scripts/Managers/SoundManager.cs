using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Sound 
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;


    [SerializeField]
    private Sound[] _bgmSounds, _sfxSounds;
    [SerializeField]
    private AudioSource _musicSource, _effectsSource;

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
        PlayMusic("Title");
    }


    public void PlayMusic(string name) 
    {
        Sound s = Array.Find(_bgmSounds, x => x.name == name);

        if (s == null) 
        {
            Debug.Log("Sound not found");
        }
        else
        {
            _musicSource.clip = s.clip;
            _musicSource.Play();
        }
    }

    public void PlaySound(string name) 
    {
        Sound s = Array.Find(_sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            _effectsSource.PlayOneShot(s.clip);
        }
    }
}
