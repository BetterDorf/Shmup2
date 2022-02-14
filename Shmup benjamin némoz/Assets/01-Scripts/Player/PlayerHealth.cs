using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : ShipHealth
{
    protected override void Die()
    {
        BackgroundEffects.instance.ChangeBackground(BackgroundEffects.Type.softPurple, 0.0f);
        Time.timeScale = 0.0f;
    }

    protected override void OnBecameInvisible()
    {
        Debug.Log("Player is out of the screen");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<ShipBehaviour>())
        {
            Die();
        }
    }

    public override void DamageEffect()
    {
        BackgroundEffects.instance.ChangeBackground(BackgroundEffects.Type.purple, 0.2f);
    }
}
