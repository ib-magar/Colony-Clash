using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IHealth
{

    protected float health;
    public float startingHealth;
    public event System.Action DiedEvent;
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
    }
    [ContextMenu("self destruct")]
    protected void Die()
    {
        if (DiedEvent != null)
            DiedEvent();   // calling die event
        GameObject.Destroy(gameObject);
    }

}
