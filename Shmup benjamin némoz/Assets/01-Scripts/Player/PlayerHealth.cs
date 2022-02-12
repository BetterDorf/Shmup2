using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : ShipHealth
{
    protected override void Die()
    {
        Debug.Log("Dead");
    }

    protected override void OnBecameInvisible()
    {
        Debug.Log("Player is out of the screen");
    }
}
