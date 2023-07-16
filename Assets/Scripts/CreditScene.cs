using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScene : MonoBehaviour
{


    private IEnumerator Start()
    {
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene(0);
    }
}
