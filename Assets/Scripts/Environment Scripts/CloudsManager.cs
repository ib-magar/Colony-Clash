using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Net;

public class CloudsManager : MonoBehaviour
{

    #region singleton 
    private static CloudsManager instance;

    // Public property to access the singleton instance
    public static CloudsManager Instance
    {
        get
        {
            // If the instance is null, try to find an existing instance in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<CloudsManager>();

                // If no instance exists in the scene, create a new GameObject and attach the script to it
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(CloudsManager).Name);
                    instance = singletonObject.AddComponent<CloudsManager>();
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


    public Transform _leftCloud;
    public Transform _rightCloud;

    private void Start()
    {
        _leftCloud.position = new Vector3(-20f, _leftCloud.position.y, 0f);
        _rightCloud.position = new Vector3(-2.81f, _rightCloud.position.y, 0f);

        OpenClouds();
    }
    public float _openTime = 3f;
    public float _closeTime = 3f;
    public void OpenClouds()
    {
        _leftCloud.DOMoveX(-27.8f, _openTime).From(-15);
        _rightCloud.DOMoveX(18.96f, _openTime).From(-1f);
    }

    public void closeClouds()
    {
        _leftCloud.DOMoveX(-4f, _closeTime);
        _rightCloud.DOMoveX(7f, _closeTime);
        
    }
}
