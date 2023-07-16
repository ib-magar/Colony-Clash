using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SugarBox : MonoBehaviour
{
    public GameObject _electricLaser;
    public GameObject _triggerEffect;
    public LayerMask _enemyLayer;
    public AudioClip _touchSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_enemyLayer == (_enemyLayer | (1 << collision.gameObject.layer)))
        {

            Instantiate(_electricLaser, transform.position, _electricLaser.transform.rotation);
            Instantiate(_triggerEffect, transform.position, _electricLaser.transform.rotation);
            //SoundManager.Instance.PlaySoundEffect(_touchSound);

            Destroy(gameObject, .1f);
        }
    }

}
