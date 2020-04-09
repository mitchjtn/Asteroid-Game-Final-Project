using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManagerScript : MonoBehaviour
{
    public int curNumAsteroid = 0;
    public int level = 0;
    public GameObject asteroidLarge;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;

    private static int[] spawnLargeAsteroid = { 0, 1, 1, 1, 1, 2,    2, 2, 2, 2, 3,   3, 3, 3, 3, 3,   15 };  
    private static int[] spawnMediumAsteroid = { 0, 2, 2, 2, 3, 3,   3, 3, 4, 4, 4,   4, 4, 5, 5, 5,   15 }; 
    private static int[] spawnSmallAsteroid = { 0, 3, 4, 4, 5, 5,    3, 4, 4, 5, 5,   4, 5, 5, 6, 6,   15 };   
    private static float[] cooldown = { 0f,10f,10f,10f,10f,10f     ,9f,9f,9f,9f,9f,   8f,8f,8f,8f,8f,  5f};  
    private static int[] wave =              { 0, 5, 5, 5, 5, 5,     7, 7, 7, 7, 7,   9, 9, 9, 9, 100,  10};                 
    public int waveNow = 1;

    public int maxLevel;

    public Text textLevel;

    public GameObject gameOverPanel;
    public GameObject newHighScorePanel;
    public InputField highScoreInput;
    public GameObject player;
    public Text highScoreListText;
    //    public GameObject LevelObject;

    public void Start()
    {
        // LevelObject = GameObject.FindGameObjectsWithTag("Level");
        player = GameObject.FindGameObjectWithTag("Player");
        
        StartNewLevel();
    }

    public void UpdateNumberofAsteroid(int change)
    {
        curNumAsteroid += change;

        //check if there is no asteroid left
        if (curNumAsteroid == 0 && waveNow == wave[level])
        {

            //new Level
            waveNow = 1;
            StartNewLevel();
        }
    }

    public void disableTextLevel()
    {
        textLevel.enabled = false;
        //LevelObject.SetActive(false);
    }

    public void StartNewLevel()
    {
        level++;
        textLevel.enabled = true;
        textLevel.text = "Level " + level;
        Invoke("disableTextLevel", 3.5f);

        Invoke("SpawnAsteroid", 4f);

    }

    public void SpawnAsteroid()
    {
        //spawnNewAsteroid
        for (int i = 1; i <= spawnLargeAsteroid[level]; i++)
        {
            StartCoroutine(Spawn(asteroidLarge, cooldown[level]));
        }

        for (int i = 1; i <= spawnMediumAsteroid[level]; i++)
        {
            StartCoroutine(Spawn(asteroidMedium, cooldown[level]));

        }
        for (int i = 1; i <= spawnSmallAsteroid[level]; i++)
        {
            StartCoroutine(Spawn(asteroidSmall, cooldown[level]));
        }
    }

    IEnumerator Spawn(GameObject asteroid, float delay)
    {
        for (int i = 1; i <= wave[level]; i++)
        {

            Vector2 spawnPosition = new Vector2(Random.Range(-35.9f, 35.9f), 20f);
            Instantiate(asteroid, spawnPosition, Quaternion.identity);
            UpdateNumberofAsteroid(1);
            //Debug.Log("Spawn Asteroid");
            waveNow = i;

            yield return new WaitForSeconds(delay);
        }

    }

    public void GameOver(int score)
    {

        if (CheckForHighScore(score))
        {
            newHighScorePanel.SetActive(true);
        }
        else
        {
            highScoreListText.text = "HIGH SCORE " + "\n\n" + PlayerPrefs.GetString("highscoreName") + "  " + PlayerPrefs.GetInt("highscore");
            gameOverPanel.SetActive(true);
        }
    }

    public void HighScoreInput()
    {
        string newInput = highScoreInput.text;
        Debug.Log(newInput);
        newHighScorePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        PlayerPrefs.SetString("highscoreName", newInput);
        player.SendMessage("SetNewHighScore");
        highScoreListText.text = "HIGH SCORE" + "\n\n" + PlayerPrefs.GetString("highscoreName") + "  " + PlayerPrefs.GetInt("highscore");
        
    }

    public bool CheckForHighScore(int score)
    {
        Debug.Log("highscore");
        int highScore = PlayerPrefs.GetInt("highscore");
        if (score > highScore)
        {
            Debug.Log("new highscore");
            return true;
        }
        return false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }


}
