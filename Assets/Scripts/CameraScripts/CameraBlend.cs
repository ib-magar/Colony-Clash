using UnityEngine;
using Cinemachine;
using System;
using static UnityEngine.Rendering.DebugUI;

public class CameraBlend : MonoBehaviour
{
    
    public CinemachineVirtualCamera[] virtualCameras;

    public CinemachineBrain cinemachineBrain;
    private int activeCameraIndex = 1;

    private void Awake()
    {
        if(Time.timeScale<1) Time.timeScale = 1;
    }
    private void Start()
    {
        LevelManager.Instance.BOSSENEMY += ChangeBossEnemyView;
        noise = shakeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        GameObject.FindObjectOfType<LevelManager>().BOSSENEMY += BossEnemyInvadine;
    }
    public Transform _bossviewPoint;
    public void ChangeBossEnemyView()
    {
        virtualCameras[2].Follow = _bossviewPoint;
    }
    public Transform _EnemyCameraPosition;
    public Transform _bossEnemyPosition;
    public void BossEnemyInvadine()
    {
        _EnemyCameraPosition = _bossEnemyPosition;
    }
    public void TransitionToCamera1()
    {
        if (activeCameraIndex != 0)
        {
            cinemachineBrain.ActiveVirtualCamera.Priority = 0;
            activeCameraIndex = 0;
            //cinemachineBrain.ActiveVirtualCamera = virtualCameras[activeCameraIndex];
            SetCameraPriority(activeCameraIndex, 10);
        }
    }

    public void TransitionToCamera2()
    {
        if (activeCameraIndex != 1)
        {
            cinemachineBrain.ActiveVirtualCamera.Priority = 0;
            activeCameraIndex = 1;
            // cinemachineBrain.ActiveVirtualCamera = virtualCameras[activeCameraIndex];
            SetCameraPriority(activeCameraIndex, 10);
        }
    }

    public void TransitionToCamera3()
    {
        if (activeCameraIndex != 2)
        {
            cinemachineBrain.ActiveVirtualCamera.Priority = 0;
            activeCameraIndex = 2;
            // cinemachineBrain.ActiveVirtualCamera = virtualCameras[activeCameraIndex];
            SetCameraPriority(activeCameraIndex, 10);
        }
    }

  
    private void SetCameraPriority(int cameraIndex, int priority)
    {
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].Priority = i == cameraIndex ? priority : 0;
        }
    }
    public Transform[] spawnPoints;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(Transform t in spawnPoints)
        {
            
            Gizmos.DrawLine(t.position - Vector3.right*500, t.position+Vector3.right*500);
        }
    }


    public CinemachineVirtualCamera shakeCamera; // Reference to the Cinemachine virtual camera
    public float shakeDuration = 0.15f; // Duration of the camera shake
    public float shakeAmplitude = 1f; // Amplitude of the camera shake

    private CinemachineBasicMultiChannelPerlin noise;


    public void ShakeCamera()
    {
        // Start the camera shake by setting the noise parameters
        noise.m_AmplitudeGain = shakeAmplitude;
        noise.m_FrequencyGain = 10f;

        // Invoke the StopShaking function to stop the camera shake after the specified duration
        Invoke(nameof(StopShaking), shakeDuration);
    }

    private void StopShaking()
    {
        // Stop the camera shake by resetting the noise parameters
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }





}
