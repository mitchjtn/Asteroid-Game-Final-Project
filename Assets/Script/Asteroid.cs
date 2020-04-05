using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float maxThrust = 11000f;
    private float maxTorque = 5000f;
    private Vector2 thrust;
    private float torque;
    public Rigidbody2D rb;

    public int asteroidSize; //1 small, 2 medium, 3 large
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;


    public float screenTop = 27f;
    public float screenBottom = -27f;
    public float screenRight = 37f;
    public float screenLeft = -37f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        torque = Random.Range(-maxTorque, maxTorque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }

    // Update is called once per frame
    void Update()
    {
        //screen Wrapping
        Vector2 newPos = transform.position;

        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit by " + other.name);
        //check bullet
        if (other.CompareTag("bullet"))
        {
            //destroy bullet
            Destroy(other.gameObject);

            //split the asteroid
            if(asteroidSize == 3) //large Asteroid
            {
                //spawn 2 medium asteroid
                GameObject asteroid1 = Instantiate(asteroidMedium, transform.position, transform.rotation);
                GameObject asteroid2 = Instantiate(asteroidMedium, transform.position, transform.rotation);
     
                Destroy(gameObject);
            }
            else if(asteroidSize == 2) //medium asteroid
            {
                //spawn 2 small asteroid
            }
            else if(asteroidSize == 1) //small asteroid
            {
                //destroy the asteroid
            }
        }
        
    }

}
