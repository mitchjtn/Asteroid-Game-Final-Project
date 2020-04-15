using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public Slider musicSlider;
    public Slider sfxSlider;
    public GameObject AudioM;

    private void Start()
    {
        AudioM = GameObject.FindWithTag("AudioManager");
        musicSlider.value = AudioM.GetComponent<AudioManagerScript>().musicVolumes;
        sfxSlider.value = AudioM.GetComponent<AudioManagerScript>().sfxVolumes;
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void setVolumeMusic(float volume)
    {
        AudioM.GetComponent<AudioManagerScript>().setMusicVolume(volume);
    }

    public void setVolumeSFX(float volume)
    {
        AudioM.GetComponent<AudioManagerScript>().setSFXVolume(volume);
    }
}
