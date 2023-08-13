using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public int _enemiesCount;

    public LevelManager _levelMg;
    private void Start()
    {
        _levelMg = GameObject.FindObjectOfType<LevelManager>();
    }
    public float speed=.2f;
    private void FixedUpdate()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }


}
