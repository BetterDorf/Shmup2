using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] int startLife = 5;
    int life;

    private void Start()
    {
        life = startLife;
    }

    //Take damages then trigger the death if at 0
    public virtual void TakeDamage(int damage, bool lethal)
    {
        life -= damage;

        if (isDead())
        {
            if (lethal)
                Die();
            else
                Arrest();
        }
        else
        {
            DamageEffect();
        }
    }

    public virtual void DamageEffect()
    {
        //TODO add flashing on sprite
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
