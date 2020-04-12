using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //karna di Unity
                                                                              // File > Build Settings, Scene Game ada
                                                                              // di index 1
    }

    public void SettingGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); // Scene Settings di index 2
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
