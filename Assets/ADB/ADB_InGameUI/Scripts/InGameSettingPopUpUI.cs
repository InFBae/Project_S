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
        // 배경음, 효과음, 감도, Cancle, 로비로 나가기, Confirm

        public static UnityEvent<float> OnMouseSensiticityControl = new UnityEvent<float>();

        // Cancle을 위한 초기값들
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
            // 시작할 때의 기본 값들을 다 저장
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
            // STart()에서 기초 값들을 다 저장해놓고 그 기초값들로 다 바꿔주면 됨
            sliders["MouseSensitivitySlider"].value = initalMouseSensitivityValue;
            sliders["BackgroundSoundSlider"].value = initalBackgroundSoundValue;
            sliders["EffectSoundSlider"].value = initalEffectSoundValue;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            GameObject.FindWithTag("Player").GetComponentInChildren<PlayerInput>().enabled = true;
            CloseUI();
        }

    }
}