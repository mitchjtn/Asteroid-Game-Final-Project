using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public void setSFX(int volume)
    {
        
    }

    public void setMusic(int volume)
    {

    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
