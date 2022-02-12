using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] float timeBetweenShots = 2.0f;
    float timeToShoot = 0.0f;

    [SerializeField] Transform shootPoint;
    [SerializeField] Object projectile;

    protected GameObject player;

    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartMoving();
        StartShooting();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        if (timeToShoot >= 0.0f)
            timeToShoot -= Time.deltaTime;

        if (timeToShoot <= 0.0f)
            Shoot();
    }

    protected virtual void StartShooting()
    {
        timeToShoot = Random.Range(timeBetweenShots / 4.0f, timeBetweenShots);
    }

    protected virtual void Shoot()
    {
        Instantiate(projectile, shootPoint.position, Quaternion.identity);
        timeToShoot = timeBetweenShots;
    }

    protected virtual void StartMoving()
    {
        rb.velocity = new Vector2(1 * speed, -0.1f * speed);
    }


    //Bounce against the side of the playfield
    protected virtual void Bounce()
    {
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bounds"))
        {
            Bounce();
        }
    }
}
