using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSoundLoop : MonoBehaviour
{

    public AudioSource _source;
    public AudioClip clip;
    public float _timerInterval;
    private IEnumerator Start()
    {
        while(true)
        {
            _source.PlayOneShot(clip);
            yield return new WaitForSeconds(_timerInterval);
        }
    }

}
