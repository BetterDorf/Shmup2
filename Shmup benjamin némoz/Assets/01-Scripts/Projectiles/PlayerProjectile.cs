using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField]float speed = 1.0f;
    [SerializeField] int damage = 3;
    [SerializeField] bool lethal = true;

    [SerializeField] bool pierce = false;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the touched collider has a shipHealth script
        ShipHealth ship;
        ship = collision.GetComponent<ShipHealth>();

        if (ship)
        {
            //Damage the enemy ship
            ship.TakeDamage(damage, lethal);

            //Survive the collision if we're meant to pierce through ships
            if (!pierce)
                DestroyProjectile();
        }
    }

    private void OnBecameInvisible()
    {
        //Destroy itself when it is offscreen
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
