using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCircleBehaviour : ShipBehaviour
{
    [SerializeField] float turnSpeed;

    protected override void StartMoving()
    {
        rb.velocity = new Vector2(Random.Range(-1, 2) * speed, Random.Range(-1, 2) * speed);
    }
}
