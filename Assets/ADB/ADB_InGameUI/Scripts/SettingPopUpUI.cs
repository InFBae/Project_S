using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SettingPopUpUI : PopUpUI
{
    // �Ҹ�, ����, Exit

    public static UnityEvent<float> OnMouseSensiticityControl = new UnityEvent<float>();

    private void Awake()
    {
        base.Awake();
        buttons["ExitButton"].onClick.AddListener(() => { Exit(); });
       // sliders["MouseSensitivitySlider"].onValueChanged.AddListener(delegate { MouseSensitivityControl(ahndabi.ADB_PlayerAimMove.cameraSensitivity); });
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
        Debug.Log("slider");
        sliders["MouseSensitivitySlider"].value = sensitivity;
        OnMouseSensiticityControl?.Invoke(sensitivity);
        //sliders["MouseSensitivitySlider"].onValueChanged.Invoke(ahndabi.ADB_PlayerAimMove.cameraSensitivity);
    }

    public void Exit()
    {
        CloseUI();
    }
}
