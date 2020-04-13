using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;



public class LeaderboadSystem : MonoBehaviour
{
    [Serializable]
    struct data
    {
        public int score;
        public string name;
    };
    List<data> scores;

    public GameObject gameOverPanel;
    public GameObject newHighScorePanel;
    public InputField highScoreInput;
    public GameObject player;
    public GameObject leaderboardPanel;

    public Text nameText;
    public Text scoreText;

    private void Start()
    {
        scores = new List<data>();
        LoadHighScores();
        
        if (SceneManager.GetActiveScene().buildIndex == 2) player = GameObject.FindGameObjectWithTag("Player");
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            //Debug.Log("print");

            nameText.text = ""; scoreText.text = "";
            for (int i = 0; i < 5; i++)
            {
                //highScoreListText.text += scores[i].name + "  " + scores[i].score + "\n";
                //Debug.Log(scores[i].name + " " + scores[i].score);
                nameText.text += scores[i].name + "\n";
                scoreText.text += scores[i].score + "\n";
            }
        }
    }

    void SetNewHighScore()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scores.mijo";
        FileStream fStream = new FileStream(path, FileMode.Create);
        binaryFormatter.Serialize(fStream, scores);
        fStream.Close();
    }
    void LoadHighScores()
    {
        Debug.Log("front");
        string path = Application.persistentDataPath + "/scores.mijo";
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fStream = new FileStream(path, FileMode.Open);
            scores = binaryFormatter.Deserialize(fStream) as List<data>;
            fStream.Close();
        }
        else
        {
            Debug.Log("else statement");
            scores.Clear();
            data defaultScore = new data
            {
                score = 10000,
                name = "LORDMIJO"
            };
            scores.Add(defaultScore);

            defaultScore = new data
            {
                score = 8000,
                name = "LORDMIJO2"
            };
            scores.Add(defaultScore);

            defaultScore = new data
            {
                score = 5000,
                name = "LORDMIJO3"
            };
            scores.Add(defaultScore);

            defaultScore = new data
            {
                score = 2000,
                name = "LORDMIJO4"
            };
            scores.Add(defaultScore);

            defaultScore = new data
            {
                score = 1000,
                name = "LORDMIJO5"
            };
            scores.Add(defaultScore);
            SetNewHighScore();
        }
    }
    public void HighScoreInput()
    {
        string newInput = highScoreInput.text;
        Debug.Log(newInput);
        newHighScorePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        InsertScore(newInput, player.GetComponent<SpaceshipControls>().score);
        SetNewHighScore();
        //highScoreListText.text = "HIGH SCORE" + "\n\n";// + PlayerPrefs.GetString("highscoreName") + "  " + PlayerPrefs.GetInt("highscore");
        
    }

    public void OpenLeaderboardPanel()
    {
        gameOverPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        nameText.text = ""; scoreText.text = "";
        for (int i = 0; i < 5; i++)
        {
            //highScoreListText.text += scores[i].name + "  " + scores[i].score + "\n";
            //Debug.Log(scores[i].name + " " + scores[i].score);
            nameText.text += scores[i].name + "\n";
            scoreText.text += scores[i].score + "\n";
        }
    }

    public void CloseLeaderboardPanel()
    {
        gameOverPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
    }

    void InsertScore(string name, int score)
    {
        data newData;
        int i;
        newData.score = score;
        newData.name = name;
        for (i = 0; scores[i].score > score; i++) ;
        scores.Insert(i, newData);
        scores.RemoveAt(5);
    }

    public bool CheckForHighScore(int score)
    {
        Debug.Log("highscore");
        foreach (data i in scores)
        {
            if (score > i.score) return true;
        }
        return false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
