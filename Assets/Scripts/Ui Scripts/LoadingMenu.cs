using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LoadingMenu : MonoBehaviour
{
    public Slider loadingBarSlider;
    public string sceneToLoad;
    public float waitTime=1.5f;
    public float loadingSpeedMultiplier = 0.5f;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Update the slider value based on the loading progress
            loadingBarSlider.value = asyncOperation.progress / 0.9f;

            // Check if the scene has finished loading
            if (asyncOperation.progress >= 0.9f)
            {
                // Activate the scene when loading is complete
                asyncOperation.allowSceneActivation = true;
            }

            // Add a delay to slow down the loading process
            yield return new WaitForSeconds(1f / loadingSpeedMultiplier);
        }
    }
}
