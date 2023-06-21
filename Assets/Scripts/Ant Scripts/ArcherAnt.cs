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
    }

    [Header("Archer Behavioiur")]
    public float _attackTime;
    public float _attackRange;
    public LayerMask _enemyLayerMask;
    public bullet _bullet;
    public Transform _spawnPoint;
    public ParticleSystem _damageParticleSystem;
    IEnumerator ArcherBehaviour()
    {
        while(true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _attackRange,_enemyLayerMask);

            if (hit.collider != null)
            {
                bullet b = Instantiate(_bullet, _spawnPoint.position, Quaternion.identity);
                b.Init(transform.right);
                //shoot a bullet
               // Debug.Log("shoot");
            }
            yield return new WaitForSeconds(_attackTime);
        }
    }
    void Death()
    {
        //Death function
    }
    void DamageEffect()
    {
        _damageParticleSystem.Play();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * _attackRange);
    }

}
