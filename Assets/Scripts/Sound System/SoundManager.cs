using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region singleton 
    private static SoundManager instance;

    // Public property to access the singleton instance
    public static SoundManager Instance
    {
        get
        {
            // If the instance is null, try to find an existing instance in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                // If no instance exists in the scene, create a new GameObject and attach the script to it
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(SoundManager).Name);
                    instance = singletonObject.AddComponent<SoundManager>();
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

    [Header("Audio sources")]
    private AudioSource _FxSource;
    
    public void PlaySoundEffect(AudioClip clip)
    {
        _FxSource.PlayOneShot(clip);
    }


}
