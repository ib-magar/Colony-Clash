using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Worker_Ant : LivingEntity
{
    public override void Start()
    {
        base.Start();
        DiedEvent += Death;
        DamageEvent += DamageEffect;
        _workPointHolder = GameObject.FindGameObjectWithTag("WorkPointHolder").transform;
        _generateCoin.AddListener( EconomySystem.Instance.AddCoins);
        
        StartCoroutine(WorkerAntBehaviour());
    }
    [Header("Events")]
    public UnityEvent<int> _generateCoin;

    [Header("Behaviour")]
    //public static List<Transform> _workPointHolder;
    public int _coinValue;
     Transform _workPointHolder;
     Transform target;
    //public float _coinCollectionTime;
    public float _speed=1f;
    public float _distanceReach;
    public float _waitTime;
    public float _rotationSpeed;
    IEnumerator WorkerAntBehaviour()
    {
        Vector2 direction;
        float angle;
        //Quaternion targetRotation;
        while(true)
        {
            target = _workPointHolder.GetChild(Random.Range(0, _workPointHolder.childCount)).transform;
              direction = (target.position - transform.position);
            /*while ((Vector2)transform.up != direction)
            {
                 angle =90f- Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                 targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                yield return null;
            }*/
            while (Vector2.Distance(target.position,transform.position)>_distanceReach && target!=null)
            {
                // Make the GameObject face the target point
                  direction = (target.position - transform.position).normalized;
                  angle =90f- Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.up = direction;

                // Move the GameObject towards the target point
                transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
                yield return null;
            }
            PlaySoundEffect(_coinCollectedSound);
            if (_generateCoin != null) _generateCoin.Invoke(_coinValue);
            yield return new WaitForSeconds(_waitTime);
        }
    }

    [Header("Audio")]
    public AudioClip _coinCollectedSound;

    void DamageEffect()
    {
       
    }
    //Death function 
    void Death()
    {
        //Death function 
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up * _distanceReach);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceReach);
    }
}
