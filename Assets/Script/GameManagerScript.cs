using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagerScript : MonoBehaviour
{
    public int curNumAsteroid = 0;
    public int level = 0;
    public GameObject asteroidLarge;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;

    private static int[] spawnLargeAsteroid = { 0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 15 };
    private static int[] spawnMediumAsteroid = { 0, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 15 };
    private static int[] spawnSmallAsteroid = { 0, 3, 4, 4, 5, 5, 3, 4, 4, 5, 5, 4, 5, 5, 6, 6, 15 };
    private static float[] cooldown = { 0f, 13f, 13f, 13f, 13f, 13f, 12f, 12f, 12f, 12f, 11f, 11f, 11f, 10f, 10f, 9f, 5f };
    private static int[] wave = { 0, 2, 3, 3, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 100, 10 };
    private int maxAsteroidinScreen = 30;

    public int maxLevel;
    public int maxAsteroidPerLevel;
    public int asteroidLevelNow = 0;

    public GameObject star;
    public int starNow;


    public Text textLevel;

    public LeaderboadSystem lb;
    public GameObject gameOverPanel;
    public GameObject newHighScorePanel;
    public InputField highScoreInput;
    private GameObject player;
    public Text highScoreListText;
    public Text scoreText;
    //    public GameObject LevelObject;

    public void Start()
    {
        //PlayerPrefs.SetInt("highscore", 0);
        // LevelObject = GameObject.FindGameObjectsWithTag("Level");
        player = GameObject.FindGameObjectWithTag("Player");
        lb = GetComponent<LeaderboadSystem>();
        StartNewLevel();
        Invoke("SpawnStar", Random.Range(2f, 7f));
    }

    public void updateStar(int change)
    {
        starNow += change;
        if (starNow == 0)
        {
            Invoke("SpawnStar", Random.Range(7f, 15f));
        }
    }

    public void SpawnStar()
    {
        starNow++;
        Vector2 spawnPosition = new Vector2(Random.Range(-33f, 33f), Random.Range(-18f, 18f));
        Instantiate(star, spawnPosition, Quaternion.identity);
    }


    public void UpdateNumberofAsteroid(int change)
    {
        curNumAsteroid += change;

        //check if there is no asteroid left
        if (curNumAsteroid == 0 && asteroidLevelNow >= maxAsteroidPerLevel)
        {
            //new Level
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
        maxAsteroidPerLevel = wave[level] * (spawnLargeAsteroid[level] + spawnMediumAsteroid[level] + spawnSmallAsteroid[level]);
        asteroidLevelNow = 0;
        maxAsteroidinScreen = (int)(maxAsteroidinScreen * 1.2f);
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
        yield return new WaitForSeconds(Random.Range(0f, 7f));
        for (int i = 1; i <= wave[level]; i++)
        {
            while (curNumAsteroid >= maxAsteroidinScreen) yield return null;
            UpdateNumberofAsteroid(1);
            Vector2 spawnPosition = new Vector2(Random.Range(-35.9f, 35.9f), 30f);
            Instantiate(asteroid, spawnPosition, Quaternion.identity);
            //Debug.Log("Spawn Asteroid");
            asteroidLevelNow++;

            yield return new WaitForSeconds(delay);
        }

    }

    public void GameOver(int score)
    {
        scoreText.text = "Score: " + score;
        if (lb.CheckForHighScore(score))
        {
            newHighScorePanel.SetActive(true);
        }
        else
        {
            //tambahin button leaderboard sama kasi tau scorenya
            //highScoreListText.text = "HIGH SCORE " + "\n\n" + PlayerPrefs.GetString("highscoreName") + "  " + PlayerPrefs.GetInt("highscore");
            gameOverPanel.SetActive(true);
        }
    }


    public void Exit()
    {
        FindObjectOfType<AudioManagerScript>().Play("btn_click");
        Application.Quit();
    }

    public void PlayAgain()
    {
        FindObjectOfType<AudioManagerScript>().Play("btn_click");
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        FindObjectOfType<AudioManagerScript>().Play("btn_click");
        SceneManager.LoadScene("MainMenu");
    }
}