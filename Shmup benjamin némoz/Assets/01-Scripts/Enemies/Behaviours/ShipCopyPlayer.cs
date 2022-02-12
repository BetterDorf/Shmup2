using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCopyPlayer : ShipBehaviour
{
    [SerializeField] float downSpeed = 0.01f;
    [SerializeField] float margin = 0.1f;

    protected override void StartMoving()
    {
        rb.velocity = Vector2.down * downSpeed;
    }

    protected override void Update()
    {
        base.Update();

        if (Mathf.Abs(player.transform.position.x - transform.position.x) > margin)
        {
            if (player.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
