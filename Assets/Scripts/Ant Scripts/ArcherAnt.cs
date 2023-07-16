using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ArcherAnt : LivingEntity
{

    public override void Start()
    {
        Physics2D.queriesStartInColliders = false;
        base.Start();
        DiedEvent += Death;

        StartCoroutine(ArcherBehaviour());
        _Anim=GetComponentInChildren<Animator>();
    }

    [Header("Archer Behavioiur")]
    public float _attackTime;
    public float _shiftTimeRandomness=.2f;
    public float _attackRange;
    public LayerMask _enemyLayerMask;
    public bullet _bullet;
    public Transform _spawnPoint;
    public ParticleSystem _damageParticleSystem;
    public float _MaxRange=7f;

    [Header("Audios")]
    public AudioClip _shootSound;

    [Header("animations")]
    public Animator _Anim;
    IEnumerator ArcherBehaviour()
    {
        while(true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right,(_MaxRange - transform.position.x) ,_enemyLayerMask);

            if (hit.collider != null)
            {
                _Anim.SetTrigger("attack");
                bullet b = Instantiate(_bullet, _spawnPoint.position, Quaternion.identity);
                b.Init(transform.right);
                PlaySoundEffect(_shootSound);
                //shoot a bullet
               // Debug.Log("shoot");
            }
            yield return new WaitForSeconds(Random.Range(_attackTime- _shiftTimeRandomness, _attackTime+ _shiftTimeRandomness));
        }
    }
    public AudioClip _deathSound;
    void Death()
    {
        //Death function
        SoundManager.Instance.PlaySoundEffect(_deathSound);
    }
    void DamageEffect()
    {
        _damageParticleSystem.Play();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right *(_MaxRange - transform.position.x));
    }

}
