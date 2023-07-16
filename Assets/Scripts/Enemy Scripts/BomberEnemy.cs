using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BomberEnemy : LivingEntity
{
    public GameplayUiScript uiScript;
    public override void Start()
    {
        uiScript=GameObject.FindObjectOfType<GameplayUiScript>();
        Physics2D.queriesStartInColliders = false;

        base.Start();
        DiedEvent += Death;
        DamageEvent+= DamageEffect;
        State = EnemyState.moving;

    }

    public EnemyState State;
    public float _speed;
    private void Update()
    {
        if (State == EnemyState.moving)
        {
            transform.position += -Vector3.right * _speed * Time.deltaTime;
        }
    }

    [Header("Behaviour")]
    public float _damageRadius;
    public float _damageAmount;
    [Header("Effects")]
    public AudioClip _damageSound;
    public GameObject _dieEffect;
    public ParticleSystem _damageParticleSystem;
    public AudioClip _dieSound;
    void DamageEffect()
    {
        uiScript.EnemiesDamageIndication(transform.position, -10);
        PlaySoundEffect(_damageSound);
        _damageParticleSystem.Play();
    }
    void Death()
    {
        LevelManager.Instance.EnemyKilled();
        //Get destroyed
        Instantiate(_dieEffect, transform.position, Quaternion.identity);
    }
    public LayerMask _antsLayer;
    public AudioSource _enemyLoopSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_antsLayer == (_antsLayer | (1 << collision.gameObject.layer)))
        {
            Blast();
        }
        if (collision.collider.CompareTag("AntHouse"))
        {
            //Game over 
            Debug.Log("Gameover");
            LevelManager.Instance.LevelFailed();
        }
        if(collision.collider.CompareTag("Trigger"))
        {
            if (_enemyLoopSound != null) _enemyLoopSound.enabled = true;
        }
    }

    
    void Blast()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _damageRadius,_antsLayer);

        foreach (Collider2D collider in colliders)
        {
            LivingEntity livingEntity = collider.GetComponent<LivingEntity>();
            if (livingEntity != null)
            {
                livingEntity.takeDamage(_damageAmount);
            }
        }
       
        //SoundManager.Instance.PlaySoundEffect(_dieSound, true);
        Die();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _damageRadius);
    }

}
