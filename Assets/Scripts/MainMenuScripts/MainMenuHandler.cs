using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(IntroHandler))]
public class MainMenuHandler : MonoBehaviour
{
    [Header("Start Menu")]
    [SerializeField] GameObject IntroObject;
    [SerializeField] GameObject MainMenuObject;
    public AudioSource _menubgm;
    public float _menuSound;

    private void Awake()
    { 
        if (Time.timeScale < 1) Time.timeScale = 1;
    }
    public float _introVolume = .2f;
    public float _MenuButtonVolume = .4f;
    private void Start()
    {
        _FxSource=gameObject.AddComponent<AudioSource>();
        _FxSource.volume = _MenuButtonVolume;
        IntroObject.SetActive(true);
        MainMenuObject.SetActive(false);
        _loadingScreen.SetActive(false);
        // GetComponent<IntroHandler>()._IntroFinished += ClickToStart;
        _menubgm.DOFade(_introVolume, 4f).From(0f);

        Debug.Log("start");   
    }

    public void FadeMenuMusic()
    {
        _menubgm.DOFade(_menuSound, 1f);
        _menubgm.GetComponent<AudioLowPassFilter>().enabled = false;
    }
    public void FadeOutMusic()
    {
        _menubgm.DOFade(_introVolume, 1f);
        _menubgm.GetComponent<AudioLowPassFilter>().enabled = true;
    }
    public void ClickToStart()
    {
        IntroObject.SetActive(false);
        MainMenuObject.SetActive(true);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Level", 1);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //StartGame();
    }

    public GameObject _loadingScreen;
    public float _GameLoadTime=3f;
    public void StartGame()
    {
        _loadingScreen.SetActive(true);
        //StartCoroutine(GameStartCoroutine());
    }
    IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(_GameLoadTime);
        SceneManager.LoadScene("Gameplay");
    }

    public AudioClip _buttonClickClip;
    private AudioSource _FxSource;
    public void ButtonClick()
    {
        _FxSource.pitch = Random.Range(.8f, 1.2f);
        _FxSource.PlayOneShot(_buttonClickClip);
    }

    public void GiveMeIronMandibles()
    {
        PlayerPrefs.SetInt("Level", 5);
        StartGame();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
