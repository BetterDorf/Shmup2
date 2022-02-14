using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : ShipHealth
{
    protected override void Die()
    {
        if (GameManager.instance)
            GameManager.instance.Lose();
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

    public override void TakeDamage(int damage, bool lethal)
    {
        base.TakeDamage(damage, lethal);

        if (GameManager.instance)
            GameManager.instance.UpdateLifes(life);
    }

    public override void DamageEffect()
    {
        base.DamageEffect();

        if (BackgroundEffects.instance != null)
            BackgroundEffects.instance.ChangeBackground(BackgroundEffects.Type.purple, damageEffectDuration);
    }
}
