using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
<<<<<<< HEAD
    public void setSFX(int volume)
    {
        
    }

    public void setMusic(int volume)
    {

=======

    public Slider musicSlider;
    public Slider sfxSlider;
    public GameObject AudioM;

    private void Start()
    {
        AudioM = GameObject.FindWithTag("AudioManager");
        musicSlider.value = AudioM.GetComponent<AudioManagerScript>().musicVolumes;
        sfxSlider.value = AudioM.GetComponent<AudioManagerScript>().sfxVolumes;
>>>>>>> Mijo
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
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
