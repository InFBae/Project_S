using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
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
        public static UnityEvent OnPlayerInputActive = new UnityEvent();

        // Cancle�� ���� �ʱⰪ��
        float initalMouseSensitivityValue;
        float initalBackgroundSoundValue;
        float initalEffectSoundValue;

        private void Awake()
        {
            base.Awake();
            buttons["ApplyButton"].onClick.AddListener(() => { Apply(); });
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

        public void Apply()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            OnPlayerInputActive?.Invoke();
            CloseUI();
        }

        public void Cancle()
        {
            // STart()���� ���� ������ �� �����س��� �� ���ʰ���� �� �ٲ���
            sliders["MouseSensitivitySlider"].value = initalMouseSensitivityValue;
            sliders["BackgroundSoundSlider"].value = initalBackgroundSoundValue;
            sliders["EffectSoundSlider"].value = initalEffectSoundValue;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            GameObject.FindWithTag("Player").GetComponentInChildren<PlayerInput>().enabled = true;
            CloseUI();
        }

    }
}