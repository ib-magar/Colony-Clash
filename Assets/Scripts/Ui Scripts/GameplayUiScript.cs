using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUiScript : MonoBehaviour
{

    [Header("Coins")]
    public TMP_Text _coinsText;
    LevelManager _levelmg;
    private void Start()
    {
        GameObject.FindObjectOfType<LevelManager>().BOSSENEMY+=EnemyInvadingWarningBoard;
        _pauseMenu.SetActive(false);
        EconomySystem.Instance._coinsUpdatedEvent.AddListener(UpdateCoins);
        _levelmg=GameObject.FindObjectOfType<LevelManager>();
        _levelmg.NewWaveEvent += UpdateWave;

        _waveProgressbar.value = 0;
    }

    public void UpdateCoins(int amount)
    {
        _coinsText.text = amount.ToString();
    }
    [Header("warning board")]
    public TMP_Text _warningText;
    public void EnemyInvadingWarningBoard()
    {
        _warningText.text = "BOSS INVADING";
    }
    private void FixedUpdate()
    {
        _waveProgressbar.value = _levelmg._enemiesKilled;
    }

    [Header("wave progress")]
    public Slider _waveProgressbar;
    private int _waveEnemiesCount;
    public void UpdateWave(int c)
    {
        _waveProgressbar.maxValue = _levelmg._currentWaveEnemiesCount;
        //_waveEnemiesCount=_levelmg._currentWaveEnemiesCount;
    }

    public GameObject _pauseMenu;
    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu"); 
    }

    [Header("coin generation indicator")]
    public InfoObject _coinGeneratePrefab;
    public void CoinGenereated(Vector3 Pos)
    {
        InfoObject c = Instantiate(_coinGeneratePrefab,transform.position,Quaternion.identity);
        c.SetRectTransformPosition(Pos);
    }

    [Header("Enemies damage indicator")]
    public InfoObject _indcator;
    public void EnemiesDamageIndication(Vector3 Pos,int damageValue)
    {

        InfoObject c = Instantiate(_indcator, transform.position, Quaternion.identity);
        c.SetRectTransformPosition(Pos,damageValue.ToString());
    }
    [Header("Out of capacity information")]
    public InfoObject _outOfCapacityPrefab;
    public void OutOfCapacityInfo()
    {
        InfoObject outOfCapacityInfo = Instantiate(_outOfCapacityPrefab);
    }
}
