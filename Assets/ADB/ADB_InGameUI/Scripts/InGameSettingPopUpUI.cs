using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ahndabi
{
    public class InGameSettingPopUpUI : PopUpUI
    {
        // �����, ȿ����, ����, Cancle, �κ�� ������, Confirm

        public static UnityEvent<float> OnMouseSensiticityControl = new UnityEvent<float>();

        // Cancle�� ���� �ʱⰪ��
        float initalMouseSensitivityValue;
        float initalBackgroundSoundValue;
        float initalEffectSoundValue;

        private void Awake()
        {
            base.Awake();
            buttons["ConfirmButton"].onClick.AddListener(() => { Confirm(); });
            buttons["CancleButton"].onClick.AddListener(() => { Cancle(); });
        }

        void OnEnable()
        {
            // ������ ���� �⺻ ������ �� ����
            initalMouseSensitivityValue = sliders["MouseSensitivitySlider"].value;
            initalBackgroundSoundValue = sliders["BackgroundSoundSlider"].value;
            initalEffectSoundValue = sliders["EffectSoundSlider"].value;
        }

        public void VolumeControl()
        {

        }

        public void MouseSensitivityControl(float sensitivity)
        {
            OnMouseSensiticityControl?.Invoke(sliders["MouseSensitivitySlider"].value);
        }

        public void Confirm()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            GameObject.FindWithTag("Player").GetComponentInChildren<PlayerInput>().enabled = true;
            CloseUI();
        }

        public void Cancle()
        {
            // STart()���� ���� ������ �� �����س��� �� ���ʰ���� �� �ٲ��ָ� ��
            sliders["MouseSensitivitySlider"].value = initalMouseSensitivityValue;
            sliders["BackgroundSoundSlider"].value = initalBackgroundSoundValue;
            sliders["EffectSoundSlider"].value = initalEffectSoundValue;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            GameObject.FindWithTag("Player").GetComponentInChildren<PlayerInput>().enabled = true;
            CloseUI();
        }

    }
}