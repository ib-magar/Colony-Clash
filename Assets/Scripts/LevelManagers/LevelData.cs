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


}
