using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCircleBehaviour : ShipBehaviour
{
    [SerializeField] float turnSpeed;
    [SerializeField] float centerDistance;

    bool clockwise = true;

    Vector2 center;

    protected override void Start()
    {
        base.Start();

        clockwise = Random.Range(0, 2) == 0;
    }

    protected override void StartMoving()
    {
        Vector2 centerLocal = new Vector2(Mathf.Sign(Random.Range(-1, 1)), -1) * centerDistance;
        center = new Vector2(centerLocal.x + transform.position.x, centerLocal.y + transform.position.y);
    }

    protected override void Update()
    {
        base.Update();

        Vector2 toCenter = new Vector2(center.x - transform.position.x, center.y - transform.position.y);
        rb.velocity = new Vector2(toCenter.y, -toCenter.x).normalized * speed * (clockwise ? 1 : -1);
    }

    protected override void Bounce()
    {
        clockwise = !clockwise;
    }
}
