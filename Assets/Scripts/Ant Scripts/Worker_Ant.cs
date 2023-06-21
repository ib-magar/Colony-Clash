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

        _workPointHolder = GameObject.FindGameObjectWithTag("WorkPointHolder").transform;
        _generateCoin.AddListener( EconomySystem.Instance.AddCoins);

        StartCoroutine(WorkerAntBehaviour());
    }
    [Header("Events")]
    public UnityEvent<int> _generateCoin;

    [Header("Behaviour")]
    //public static List<Transform> _workPointHolder;
    public int _coinValue;
    public Transform _workPointHolder;
    public Transform target;
    //public float _coinCollectionTime;
    public float _speed=1f;
    public float _distanceReach;
    public float _waitTime;
    
    IEnumerator WorkerAntBehaviour()
    {
        while(true)
        {
                 target = _workPointHolder.GetChild(Random.Range(0, _workPointHolder.childCount)).transform;
                Vector3 direction = target.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            while (Vector2.Distance(target.position,transform.position)>_distanceReach && target!=null)
            {
                // Make the GameObject face the target point

                // Move the GameObject towards the target point
                transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
                yield return null;
            }
            if (_generateCoin != null) _generateCoin.Invoke(_coinValue);
            yield return new WaitForSeconds(_waitTime);
        }
    }


    //Death function 
    void Death()
    {
        //Death function 
    }

}
