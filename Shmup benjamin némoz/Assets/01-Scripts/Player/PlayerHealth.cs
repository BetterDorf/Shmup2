using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : ShipHealth
{
    [SerializeField] float invincibleTime = 0.5f;
    bool invincible = false;

    protected override void Die()
    {
        GetComponent<AudioSource>().pitch = 1;
        GetComponent<AudioSource>().Play();

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
        if (invincible)
            return;

        base.TakeDamage(damage, lethal);

        if (GameManager.instance)
            GameManager.instance.UpdateLifes(life);
    }

    IEnumerator IFrames()
    {
        invincible = true;
        Color original = sp.color;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a / 2.0f);

        yield return new WaitForSeconds(invincibleTime);

        sp.color = original;
        invincible = false;
    }

    public override void DamageEffect()
    {
        StartCoroutine(IFrames()); 
        base.DamageEffect();

        GetComponent<AudioSource>().pitch = 3;
        GetComponent<AudioSource>().Play();

        if (BackgroundEffects.instance != null)
            BackgroundEffects.instance.ChangeBackground(BackgroundEffects.Type.blu, damageEffectDuration);
    }
}
