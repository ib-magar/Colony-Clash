using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyState
{
    moving,Attacking,Dead
}

public class Termites : LivingEntity
{

    public override void Start()
    {
        Physics2D.queriesStartInColliders = false;

        base.Start();
        DiedEvent += Death;
        DamageEvent += DamageEffect;
        State = EnemyState.moving;
        uiScript=GameObject.FindObjectOfType<GameplayUiScript>();
        _animator=GetComponentInChildren<Animator>();
        StartCoroutine(Behaviour());
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
    public float _damageAmount;
    public float _CheckTime;
    public float _checkDistance;
    public LayerMask _AntLayerMask;
    public ParticleSystem _damageParticleSystem;

    [Header("Animations")]
    [SerializeField] Animator _animator;
    IEnumerator Behaviour()
    {
        while(true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.right, _checkDistance, _AntLayerMask);

            if (hit.collider != null && hit.collider.TryGetComponent(out LivingEntity _ant))
            {
                State = EnemyState.Attacking;
                if(_animator!=null) _animator.SetTrigger("attack");

                _ant.takeDamage(_damageAmount);
            }
            yield return new WaitForSeconds(_CheckTime);        //wait for the attack animation time later.
            State = EnemyState.moving;
        }
    }
    [Header("audios")]
    public AudioClip _damageSound;
    GameplayUiScript uiScript;
    void DamageEffect()
    {
        uiScript.EnemiesDamageIndication(transform.position,-10);
        PlaySoundEffect(_damageSound);
        if(_damageParticleSystem!=null)
        _damageParticleSystem.Play();
    }
    [Header("Effects")]
    public GameObject _dieEffect;
    public AudioClip _dieSound;
    void Death()
    {
        SoundManager.Instance.PlaySoundEffect(_dieSound, true);
        LevelManager.Instance.EnemyKilled();
        //Get destroyed
       Instantiate(_dieEffect, transform.position, Quaternion.identity);
    }

    public AudioSource _enemyLoopSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("AntHouse"))
        {
            //Game over 
            LevelManager.Instance.LevelFailed();
        }
        if(collision.collider.CompareTag("Trigger"))
        {
            if (_enemyLoopSound != null) _enemyLoopSound.enabled = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (-transform.right) * _checkDistance);
    }

}
