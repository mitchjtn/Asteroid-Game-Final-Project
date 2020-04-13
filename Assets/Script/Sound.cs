using UnityEngine.Audio;
using UnityEngine;
using System.Diagnostics;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
    public bool isMusic;
    public bool isSfx;

    [HideInInspector]
    public AudioSource source;
}
