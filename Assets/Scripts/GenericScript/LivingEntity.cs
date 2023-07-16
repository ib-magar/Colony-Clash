using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//When inheriting from this script make sure to make start function override and subscribe to the dieEvent

public class LivingEntity : MonoBehaviour, IHealth, ISoundEffects
{
    [Header("Health")]
    protected float health;
    public float startingHealth;
    public event System.Action DiedEvent;
    public event System.Action DamageEvent;
    public virtual void Start()
    {
        health = startingHealth;
        _fxsource = gameObject.AddComponent<AudioSource>();
        _fxsource.volume = _startVolume;
        _fxsource.loop = false;
        _fxsource.playOnAwake = false;

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

        if (transform.parent != null) transform.parent.gameObject.layer = 6; //block layer
        GameObject.Destroy(gameObject);
    }
    protected void Damage()
    {
        if (DamageEvent != null) DamageEvent();
    }
    internal float ReturnHealth() { return health; }

    [Header("Audio")]
    protected AudioSource _fxsource;
    [Range(0, 1)] public float _startVolume = .5f;
    public Vector2 pitchRange = new Vector2(.8f, 1.2f);
    public void PlaySoundEffect(AudioClip clip)
    {
        _fxsource.pitch=Random.Range(pitchRange.x,pitchRange.y);
        _fxsource.PlayOneShot(clip);
    }
}
