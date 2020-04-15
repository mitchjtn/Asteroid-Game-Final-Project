using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManagerScript : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManagerScript instance;

    [Range(0f, 1f)]
    public float musicVolumes = 1f;
    [Range(0f, 1f)]
    public float sfxVolumes = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    private void Start()
    {
        foreach(Sound s in sounds)
        {
            s.source.volume = 1f;
        }
        Play("Theme");
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Sound " + s + "not found");
            return;
        }

        if (PauseMenu.gameIsPaused == false)
            s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + s + "not found");
            return;
        }
        s.source.Stop();
    }

    public void setMusicVolume(float volume)
    {
        foreach(Sound s in sounds)
        {
            if(s.isMusic == true)
            {
                musicVolumes = volume;
                s.source.volume = volume;
            }
        }
    }

    public void setSFXVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            if (s.isSfx == true)
            {
                sfxVolumes = volume;
                s.source.volume = volume;
            }
        }
    }
}
