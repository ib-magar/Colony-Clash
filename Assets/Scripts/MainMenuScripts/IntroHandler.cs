using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroHandler : MonoBehaviour
{

    public float _introTime;
    public event Action _IntroFinished;
    public GameObject clickToStartCanvas;
    private IEnumerator Start()
    {
        clickToStartCanvas.SetActive(false);

        _IntroFinished += enableClickToStart;
        yield return new WaitForSecondsRealtime(_introTime);
        if(_IntroFinished!=null)
        {
            _IntroFinished();
        }
    }
    public void enableClickToStart()
    {
        clickToStartCanvas.SetActive(true);
    }
    public void ButtonClicked()
    {
        clickToStartCanvas.SetActive(false);
    }

}
