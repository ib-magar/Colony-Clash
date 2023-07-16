using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

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
    private IEnumerator Start()
    {
        _FxSource=gameObject.AddComponent<AudioSource>();
        yield return new WaitForSeconds(3f);
        StartBgm();
    }
    public void PlaySoundEffect(AudioClip clip,bool _pitchVariation=true,float volume=.5f)
    {
        if(_pitchVariation)
        {
            _FxSource.pitch = Random.Range(.8f, 1.2f);
        }
        else
        {
            _FxSource.pitch = 1;
        }

        _FxSource.PlayOneShot(clip);
    }

    [Header("Bgm")]
    public AudioSource _bgmSource;
    public float _bgmVolume;
    public void StartBgm()
    {
        float _currentBgmVolume = _bgmSource.volume;
        _bgmSource.DOFade(_bgmVolume, 7f).From(0f);
    }
    public AudioClip _buttonClickClip;
    public void ButtonClick()
    {
        _FxSource.pitch = Random.Range(.8f, 1.2f);
        _FxSource.PlayOneShot(_buttonClickClip);
    }

}
