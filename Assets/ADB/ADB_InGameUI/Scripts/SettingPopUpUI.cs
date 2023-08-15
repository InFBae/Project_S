using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class SettingPopUpUI : PopUpUI
{
    // 소리, 감도, Exit

    public static UnityEvent<float> OnMouseSensiticityControl;

    private void Awake()
    {
        base.Awake();
        buttons["ExitButton"].onClick.AddListener(() => { Exit(); });
        sliders["MouseSensitivitySlider"].value = ahndabi.ADB_PlayerAimMove.cameraSensitivity;
        // 오류 sliders["MpsueSensitivitySlider"].onValueChanged.AddListener(() => { VolumeControl(); });
    }

    private void OnEnable()
    {
        //OnMouseSensiticityControl.AddListener(MouseSensitivityControl(ahndabi.ADB_PlayerAimMove.cameraSensitivity));
    }

    public void VolumeControl()
    {

    }

    public void MouseSensitivityControl(float sensitivity)
    {
        // PlayerAimMove 스크립트에 있는 Camera Sensitivity를 변동시켜주면 됨
        sliders["MouseSensitivity"].value = sensitivity;
    }

    public void Exit()
    {
        CloseUI();
    }
}
