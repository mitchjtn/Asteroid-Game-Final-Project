using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceshipControls : MonoBehaviour
{
    public float moveSpeed =  50f;
    public float turnSpeed = -40f;
    public float boost = 50f;
    Vector2 movement;
    Vector2 mousePos;    

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
    
    public GameManagerScript gm;

    private Rigidbody2D rb;
    public Camera cam;

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
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //boost
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * boost, ForceMode2D.Impulse);
        }

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
        
        //rb.AddTorque(movement.x * turnSpeed * Time.fixedDeltaTime);
        //rb.MovePosition(rb.position + movement * 15* Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }

    public void Respawn()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.color = inColor;
        Invoke("Invulnerable", 2f);
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
        if (collision.relativeVelocity.sqrMagnitude > deathForce * deathForce)
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
                CancelInvoke();
                gm.GameOver(score);
            }
        }
    }

    public void SetNewHighScore()
    {
        PlayerPrefs.SetInt("highscore", score);
    }

    public void scorePoints(int points)
    {
        score += points;
        scoreText.text = "Score : " + score;
    }
}
