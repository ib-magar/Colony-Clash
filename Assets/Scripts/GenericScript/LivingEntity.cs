using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When inheriting from this script make sure to make start function override and subscribe to the dieEvent

public class LivingEntity : MonoBehaviour, IHealth
{

    protected float health;
    public float startingHealth;
    public event System.Action DiedEvent;
    public event System.Action DamageEvent;
    public virtual void Start()
    {
        health = startingHealth;
    }

    //Interface function implementation
    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
        else
        {
            Damage();
        }
    }
    [ContextMenu("self destruct")]
    protected void Die()
    {
        if (DiedEvent != null)
            DiedEvent();   // calling die event
        GameObject.Destroy(gameObject);
    }

    protected void Damage()
    {
        if (DamageEvent != null) DamageEvent();
    }
}
