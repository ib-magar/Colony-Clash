using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region singleton 
    private static LevelManager instance;

    // Public property to access the singleton instance
    public static LevelManager Instance
    {
        get
        {
            // If the instance is null, try to find an existing instance in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();

                // If no instance exists in the scene, create a new GameObject and attach the script to it
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(LevelManager).Name);
                    instance = singletonObject.AddComponent<LevelManager>();
                }

                // Ensure that the singleton instance persists across scene changes
                //DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
    private void Awake()
    {
        
        // If an instance already exists in the scene and it's not the current instance, destroy it
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
           //PlayerPrefs.SetInt("Level", 4);
    }
    #endregion

    public event Action BOSSENEMY;
    public event Action<int> NewWaveEvent;
    public event Action LevelFailedEvent;
    public int currentLevel;
    public float _beginWaitTime;
    [Header("Levels")]
    public GameObject[] _Levels;
    private IEnumerator Start()
    {
        _BossEnemy.SetActive(false);
        _cameraMg = GameObject.FindObjectOfType<CameraBlend>();
        currentLevel = PlayerPrefs.GetInt("Level");
        
        yield return new WaitForSeconds(_beginWaitTime);
        StartCoroutine(showLevelWarning());
    }
    public GameObject _levelWarningSign;
    public float _LevelLoadTime;
    private CameraBlend _cameraMg;
    public int _currentWaveEnemiesCount=0;
    public int _enemiesKilled = 0;

    [Header("BOss enemy")]
    public GameObject _BossEnemy;
    public bool isBossLevel = false;
    IEnumerator showLevelWarning()
    {
        float _cameraAdjustTime = _LevelLoadTime / 2f;
        _levelWarningSign.SetActive(true);
        //Update UI
        yield return new WaitForSeconds(_LevelLoadTime / 2f);
        _cameraMg.TransitionToCamera3();
        if(currentLevel<=_Levels.Length)
        {
        for(int i=0;i<_Levels.Length;i++)
        {
            if (i == currentLevel-1)
            {
                _Levels[i].SetActive(true);
                _currentWaveEnemiesCount= _Levels[i].GetComponent<LevelData>()._enemiesCount;
            }
            else
                {
                    //if (_Levels[i]!=null)
               _Levels[i].SetActive(false);
                }
        }
        }
        else
        {
            if(BOSSENEMY!=null) { BOSSENEMY(); }
            isBossLevel = true;
            _BossEnemy.SetActive(true);
            _cameraAdjustTime = 6f;
        }

        yield return new WaitForSeconds(_cameraAdjustTime);
        _cameraMg.TransitionToCamera2();
        _levelWarningSign.SetActive(false);
        if (NewWaveEvent != null) NewWaveEvent(currentLevel);
    }
    void StartBattle()
    {

    }
    public void EnemyKilled()
    {
        if (!isBossLevel)
        {
            _enemiesKilled++;
            if (_enemiesKilled >= _currentWaveEnemiesCount)
            {
                currentLevel++;
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

                if (currentLevel > _Levels.Length)
                {
                    //BOSS ENEMY INTRO
                }
                else
                {
                    // Debug.Log("Level Passed");

                    //do something on level passed.
                    //level passed
                    //_Levels[currentLevel - 1].SetActive(false);
                }
                StartCoroutine(NextLevel());
            }
            //do something on enemy killed
        }
    }
    public AudioClip _levelcompleteClip;
    private IEnumerator NextLevel()
    {
        //Clouds cross
        SoundManager.Instance.PlaySoundEffect(_levelcompleteClip,true);
        CloudsManager.Instance.closeClouds();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LevelFailed()
    {
        StartCoroutine(LevelFailedCoroutine());
        if(LevelFailedEvent!=null) LevelFailedEvent();
        CloudsManager.Instance.closeClouds();

    }
    IEnumerator LevelFailedCoroutine()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        
    }
    public void checkEnemyCount()
    {
        //update every time a enemy dies.
    }

}
