using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public bool settingIsOpen = false;
    public GameObject pauseMenuUI;
    public GameObject SettingMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && settingIsOpen == false)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void openSetting()
    {
        pauseMenuUI.SetActive(false);
        SettingMenuUI.SetActive(true);
        settingIsOpen = true;
    }

    public void closeSetting()
    {
        pauseMenuUI.SetActive(true);
        SettingMenuUI.SetActive(false);
        settingIsOpen = false;
    }

}