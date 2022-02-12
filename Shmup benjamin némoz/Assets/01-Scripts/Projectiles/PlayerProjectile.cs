using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : EnemyProjectile
{
    protected override void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }
}
