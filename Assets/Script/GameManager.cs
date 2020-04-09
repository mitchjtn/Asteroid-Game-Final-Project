using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int curNumAsteroid = 0;
    public int level = 0;
    public GameObject asteroidLarge;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;

    public static int[] spawnLargeAsteroid = {0, 1, 1, 1, 1, 2 };   //1, 1, 1, 1, 2, 
    public static int[] spawnMediumAsteroid = { 0, 2, 2, 3, 3, 3 }; //2, 2, 3, 3, 3
    public static int[] spawnSmallAsteroid = {0, 3, 5, 4, 4, 5 };   //3, 5, 4, 4, 5
    public static float[] cooldown = {0f, 10f, 10f, 10f, 9f, 9f};  //10,10,10,9, 9
    public static int[] wave = {0, 2, 2, 5, 5, 5};                  //5, 5, 5, 5, 5, 
    public int waveNow = 1;

    public Text textLevel;
//    public GameObject LevelObject;

    public void Start()
    {
       // LevelObject = GameObject.FindGameObjectsWithTag("Level");
        StartNewLevel();
    }

    public void UpdateNumberofAsteroid(int change)
    {
        curNumAsteroid += change;

        //check if there is no asteroid left
        if(curNumAsteroid == 0 && waveNow == wave[level])
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
        textLevel.enabled= true;
        textLevel.text = "Level " + level;
        Invoke("disableTextLevel", 3.5f);

        Invoke("SpawnAsteroid", 4f);
       
    }

    public void SpawnAsteroid()
    {
        //spawnNewAsteroid
        for(int i = 1; i <= spawnLargeAsteroid[level]; i++)
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
        for(int i = 1; i <= wave[level]; i++)
        {

            Vector2 spawnPosition = new Vector2(Random.Range(-35.9f, 35.9f), 20f);
            Instantiate(asteroid, spawnPosition, Quaternion.identity);
            UpdateNumberofAsteroid(1);
            //Debug.Log("Spawn Asteroid");
            waveNow = i;

            yield return new WaitForSeconds(delay);
        }

    } 

    public bool CheckForHighScore(int score)
    {
        Debug.Log("highscore");
        int highScore = PlayerPrefs.GetInt("highscore");
        if(score > highScore)
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

}
