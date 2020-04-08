using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipControls : MonoBehaviour
{
    public float moveSpeed =  50f;
    public float turnSpeed = -40f;
    public float deathForce = 5f;

    public float screenTop = 21f;
    public float screenBottom = -21f;
    public float screenRight = 36f;
    public float screenLeft = -36f;
    public bool invul;
    public int score;
    public int lives;

    public Text scoreText;
    public Text livesText;
    public GameObject gameOverPanel;

    private Rigidbody2D rb;

    public Color inColor;
    public Color normalColor;

    Vector2 movement;

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
                gameOverPanel.SetActive(true);
            }

            
        }
    }

    public void GameOver()
    {
        CancelInvoke();
    }

    public void scorePoints(int points)
    {
        score += points;
        scoreText.text = "Score : " + score;
    }
}
