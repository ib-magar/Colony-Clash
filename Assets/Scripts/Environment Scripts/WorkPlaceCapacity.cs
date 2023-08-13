using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlaceCapacity : MonoBehaviour
{
    #region singleton 
    private static WorkPlaceCapacity instance;

    // Public property to access the singleton instance
    public static WorkPlaceCapacity Instance
    {
        get
        {
            // If the instance is null, try to find an existing instance in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<WorkPlaceCapacity>();

                // If no instance exists in the scene, create a new GameObject and attach the script to it
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(WorkPlaceCapacity).Name);
                    instance = singletonObject.AddComponent<WorkPlaceCapacity>();
                }

                // Ensure that the singleton instance persists across scene changes
                //DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
    private void Awake()
    {

        // If an instance already exists in the scene and it's not the current instance, destroy it
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        //PlayerPrefs.SetInt("Level", 4);
    }
    #endregion

    public int[] WorkerAntsCapacityArray;
    public int _currentworkerAntsCapacity;
    public int _currentWorkerAntCount=1;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);
        _currentworkerAntsCapacity = WorkerAntsCapacityArray[LevelManager.Instance.currentLevel-1];
    }

    public bool isCapacityFull()
    {
        if (_currentWorkerAntCount >= _currentworkerAntsCapacity) return true;
        return false;
    }
    public void UpdateWorkerAntCapacity()=> _currentWorkerAntCount++;
}
