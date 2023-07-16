using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerAnt : LivingEntity
{
    public bool _isMined;
    public override void Start()
    {
        base.Start();
        DiedEvent += Death;
        DamageEvent += DamageEffect;

        _isMined = false;
        StartCoroutine(Behavior());
    }
    [Header("Behavior")]
    public float _timeToMine;

    IEnumerator Behavior()
    {
        yield return new WaitForSeconds(_timeToMine - 1f);
        transform.DOScale(Vector2.zero, .6f).OnComplete(() =>
        {
            transform.DOScale(Vector2.one * .2f, .2f);
        }
        );
        _isMined = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //blast
        if (_isMined)
        {
            if (collision.collider.TryGetComponent(out LivingEntity entity))
            {
                entity.takeDamage(100f);
                Die();
            }
        }
    }
    [Header("Effects")]
    public GameObject _deathEffect;
    void Death()
    {
        //die effect
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
    }
    void DamageEffect()
    {
        //damage effect
       
    }

}
