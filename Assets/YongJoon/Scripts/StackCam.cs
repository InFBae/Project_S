using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StackCam : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] Camera playerCam;
    [SerializeField] Camera FPSCam;
    // Start is called before the first frame update
    private void OnEnable()
    {
        var cameraData = mainCam.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(playerCam);
        cameraData.cameraStack.Add(FPSCam);
    }
}
