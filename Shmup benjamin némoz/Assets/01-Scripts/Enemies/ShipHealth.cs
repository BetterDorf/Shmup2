using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    Color hurtColor = Color.red;
    Color baseColor;

    [SerializeField] int startLife = 5;
    protected int life;

    [SerializeField] protected float damageEffectDuration = 0.2f;
    SpriteRenderer sp;

    private void Start()
    {
        //Grab the renderer that makes the visuals
        sp = GetComponent<SpriteRenderer>();
        if (!sp)
            sp = GetComponentInChildren<SpriteRenderer>();

        baseColor = sp.color;
        life = startLife;
    }

    //Take damages then trigger the death if at 0
    public virtual void TakeDamage(int damage, bool lethal)
    {
        DamageEffect();

        life -= damage;
        if (life < 0)
            life = 0;

        if (isDead())
        {
            if (lethal)
                Die();
            else
                Arrest();
        }
    }

    public virtual void DamageEffect()
    {
        StartCoroutine(hurtChangeColor());
    }

    IEnumerator hurtChangeColor()
    {
        sp.color = hurtColor;
        yield return new WaitForSeconds(damageEffectDuration);

        sp.color = baseColor;
    }

    protected bool isDead()
    {
        if (life <= 0)
            return true;
        else
            return false;
    }

    protected virtual void Die()
    {
        GetComponent<ShipDestruction>().LaunchDestructionEffect();
        Destroy(gameObject);
    }

    protected void Arrest()
    {
        Debug.Log(name + " is arrested");
    }

    protected virtual void OnBecameInvisible()
    {
       Destroy(gameObject);
    }
}
