using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour
{    
    [SerializeField]
    private GameObject[] Cameras;
    [SerializeField]
    private Text CameraInfo;

    private int CurrentCameraIndex;

    private void Awake()
    {
        TurnOffAllCameras();
        ActivateNextCamera();
        ResetCameraIndex();
    }    

    public void SwitchCamera()
    {
        CurrentCameraIndex++;

        TurnOffAllCameras();

        if (CurrentCameraIndex >= Cameras.Length)
        {
            ResetCameraIndex();
        }

        ActivateNextCamera();     
    }

    private void TurnOffAllCameras()
    {
        foreach (GameObject camera in Cameras)
        {
            camera.SetActive(false);
        }
    }

    private void ActivateNextCamera()
    {
        GameObject currentCamera = Cameras[CurrentCameraIndex];
        currentCamera.SetActive(true);

        CameraInfo.text = string.Concat("Now watching: ", currentCamera.name);
    }

    private void ResetCameraIndex()
    {
        CurrentCameraIndex = 0;
    }    
}
