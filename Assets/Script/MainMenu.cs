using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        FindObjectOfType<AudioManagerScript>().Play("btn_click");
       SceneManager.LoadScene("Game");
    }

    public void SettingGame()
    {
        FindObjectOfType<AudioManagerScript>().Play("btn_click");
        SceneManager.LoadScene("Settings");
    }

    public void Leaderboard()
    {
        FindObjectOfType<AudioManagerScript>().Play("btn_click");
        SceneManager.LoadScene("Leaderboard");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManagerScript>().Play("btn_click");
        Debug.Log("Quit!");
        Application.Quit();
    }
}
