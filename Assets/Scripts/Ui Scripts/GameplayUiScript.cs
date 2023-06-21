using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class GameplayUiScript : MonoBehaviour
{

    [Header("Coins")]
    public TMP_Text _coinsText;

    private void Start()
    {
        EconomySystem.Instance._coinsUpdatedEvent.AddListener(UpdateCoins);
    }
    public void UpdateCoins(int amount)
    {
        _coinsText.text = amount.ToString();
    }
    
}
