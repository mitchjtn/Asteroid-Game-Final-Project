using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    private float bulletForce = 50f;
    public float delay = 10000f;
    public float shootTime;
    SpaceshipControls sc;

    private void Start()
    {
        sc = GetComponent<SpaceshipControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Mouse0) ) && sc.invul == false && Time.time >= shootTime)
        {
            shootTime = Time.time + delay;
            Shoot();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) shootTime = Time.time;
           
    }

    void Shoot()
    {
        //fire and make bullet
        FindObjectOfType<AudioManagerScript>().Play("Laser");
        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
        Destroy(newBullet, 3.0f);
       
    }
}
