using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using TMPro.Examples;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class InfoObject : MonoBehaviour
{

    public RectTransform targetRectTransform;
    public TMP_Text text;
    public void SetRectTransformPosition(Vector3 position, string value = "+5")
    {
        
            // Convert the world position to a screen position
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
            targetRectTransform.position = screenPosition;
            text.text = value;
        
    }

    private IEnumerator  Start()
    {
        yield return new WaitForSeconds(.1f);
        targetRectTransform.DOMoveY(targetRectTransform.position.y + 300f, 3f).OnStart(() =>
        {
            text.DOFade(0f, 3f);
            //valueImage.DOFade(0f, 3f);
        }).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

}
