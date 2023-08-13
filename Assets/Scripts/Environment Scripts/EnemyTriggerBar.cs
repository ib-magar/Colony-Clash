using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerBar : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<LivingEntity>(out LivingEntity r))
        {
            r.enabled = true;
            r.transform.parent = null;
            //r.transform.position = r.transform.position + Vector3.left * 2f;
        }

    }

}
