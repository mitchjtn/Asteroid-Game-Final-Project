using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceshipControls : MonoBehaviour
{
    public float moveSpeed =  50f;
    public float turnSpeed = -40f;
    Vector2 movement;

    public float deathForce = 5f;

    public bool invul;
    public int score;
    public int lives;

    public float screenTop = 21f;
    public float screenBottom = -21f;
    public float screenRight = 36f;
    public float screenLeft = -36f;

    public Text scoreText;
    public Text livesText;
    public GameObject gameOverPanel;
    public GameObject newHighScorePanel;
    public InputField highScoreInput;
    public Text highScoreListText;
    public GameManager gm;

    private Rigidbody2D rb;

    public Color inColor;
    public Color normalColor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        score = 0;
        //lives = 3;

        scoreText.text = "Score : " + score;
        livesText.text = "Lives : " + lives;

        invul = true;
        Respawn();
    }



    // Update is called once per frame
    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //screen wrapping
        Vector2 newPos = transform.position;

        if(transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        if(transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }


        transform.position = newPos;
    }
    
    void FixedUpdate()
    {
        rb.AddForce(transform.up * movement.y * moveSpeed * Time.fixedDeltaTime);
        rb.AddTorque(movement.x * turnSpeed * Time.fixedDeltaTime);

        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Respawn()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.color = inColor;
        Invoke("Invulnerable", 4f);
    }

    public void Invulnerable()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = normalColor;
        invul = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if(collision.relativeVelocity.sqrMagnitude > deathForce * deathForce)
        {
            Debug.Log("Death");
            lives--;
            livesText.text = "Lives : " + lives;

            //respawn
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            invul = true;
            Invoke("Respawn", 2f);

            if (lives <= 0)
            {
                //GameOver
                GameOver();
            }
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameOver()
    {
        CancelInvoke();
        
        if(gm.CheckForHighScore(score))
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
        PlayerPrefs.SetInt("highscore", score);
        highScoreListText.text = "HIGH SCORE" + "\n\n" + PlayerPrefs.GetString("highscoreName") + "  " + PlayerPrefs.GetInt("highscore");
    }

    public void scorePoints(int points)
    {
        score += points;
        scoreText.text = "Score : " + score;
    }
}
