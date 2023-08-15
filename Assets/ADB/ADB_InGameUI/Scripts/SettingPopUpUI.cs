using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class SettingPopUpUI : PopUpUI
{
    // �Ҹ�, ����, Exit

    public static UnityEvent<float> OnMouseSensiticityControl;

    private void Awake()
    {
        base.Awake();
        buttons["ExitButton"].onClick.AddListener(() => { Exit(); });
        sliders["MouseSensitivitySlider"].value = ahndabi.ADB_PlayerAimMove.cameraSensitivity;
        // ���� sliders["MpsueSensitivitySlider"].onValueChanged.AddListener(() => { VolumeControl(); });
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
        // PlayerAimMove ��ũ��Ʈ�� �ִ� Camera Sensitivity�� ���������ָ� ��
        sliders["MouseSensitivity"].value = sensitivity;
    }

    public void Exit()
    {
        CloseUI();
    }
}
