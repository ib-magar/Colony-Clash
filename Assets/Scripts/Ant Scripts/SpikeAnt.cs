using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAnt : LivingEntity
{

    public override void Start()
    {
        Physics2D.queriesStartInColliders = false;

        base.Start();
        DiedEvent += Death;
        DamageEvent += DamageEffect;
        StartCoroutine(Behavior());
        StartCoroutine(shake());
    }

    [Header("Behavior")]
    public float _blastTime;
    public float _targetScale;
    public float _blastRange;
    public LayerMask _targetLayerMask;
    public float _damageAmount;
    IEnumerator Behavior()
    {
        transform.DOScale(Vector3.one * _targetScale, _blastTime);
        yield return new WaitForSeconds(_blastTime);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _blastRange, _targetLayerMask);

        foreach (Collider2D collider in colliders)
        {
            if(collider.TryGetComponent(out LivingEntity enemy))
            {
                enemy.takeDamage(_damageAmount);     //max damage
            }
        }
        Die();
    }
    [Header("shake")]
    public float _shakeTime;
    [Range(0,1)] public float _strength;
    [Range(1,10)] public int _vibrato;
    [Range(0,90)]  public float _randomness;
    IEnumerator shake()
    {
        while(true)
        {
            transform.DOShakePosition(_shakeTime, _strength, _vibrato, _randomness).SetEase(Ease.Linear);
            yield return new WaitForSeconds(_shakeTime);
        }
    }
    [Header("Effects")]
    public GameObject _dieEffect;
    public AudioClip _BlastSound;
    void Death()
    {
       // SoundManager.Instance.PlaySoundEffect(_BlastSound, true);
        //Die 
        Instantiate(_dieEffect, transform.position, Quaternion.identity);
       
    }
    void DamageEffect()
    {
        //Damage Effect
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _blastRange);
    }
}
