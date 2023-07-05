using UnityEngine;
using Cinemachine;

public class CameraBlend : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras;

    public CinemachineBrain cinemachineBrain;
    private int activeCameraIndex = 1;

    private void Start()
    {
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
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
}
