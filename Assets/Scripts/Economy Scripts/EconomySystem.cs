using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;
using JetBrains.Annotations;
using UnityEngine.Events;

public class EconomySystem : MonoBehaviour
{
    #region singleton 

    private static EconomySystem instance;

    // Public property to access the singleton instance
    public static EconomySystem Instance
    {
        get
        {
            // If the instance is null, try to find an existing instance in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<EconomySystem>();

                // If no instance exists in the scene, create a new GameObject and attach the script to it
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(EconomySystem).Name);
                    instance = singletonObject.AddComponent<EconomySystem>();
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
    }

    #endregion


    public Dictionary<string, int> _entityValue=new Dictionary<string, int>();

    [Header("Economy Holder")]
    [SerializeField] int _currentCoins;
    public int startingCoinsValue;

    [Header("Events")]
    public  UnityEvent<int> _coinsUpdatedEvent;     //add coins reduced sound  and UI update 
    //public UnityEvent _coinsAddedEvent;
    private void Start()
    {
        _currentCoins = startingCoinsValue;    
        _source=GetComponent<AudioSource>();
    }

    public bool buyEntity(string entityName)
    {
        if(_entityValue.ContainsKey(entityName))
        {
            int _requiredValue = _entityValue[entityName];
            if(_requiredValue > 0 && _requiredValue<_currentCoins)
            {
                //grant the entity
                _currentCoins-= _requiredValue;
                if (_coinsUpdatedEvent != null) _coinsUpdatedEvent.Invoke(_currentCoins);
                //PlaySoundEffect(_coinReducedSound);
                return true;
            }
        }
        return false;
    }
    public void AddCoins(int amount)
    {
        if(amount>0)
        {
            _currentCoins+= amount;
            if (_coinsUpdatedEvent != null) _coinsUpdatedEvent.Invoke(_currentCoins);
            //PlaySoundEffect(_coinCollectedSound);
        }
    }
    [Header("audios")]
    public AudioClip _coinCollectedSound;
    public AudioClip _coinReducedSound;
    private AudioSource _source;
    void PlaySoundEffect(AudioClip clip)
    {
        _source.pitch = UnityEngine.Random.Range(.8f, 1.3f);
        _source.PlayOneShot(clip);
    }
}
