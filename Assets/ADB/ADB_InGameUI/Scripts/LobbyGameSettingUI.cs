using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

namespace ahndabi
{
    public class LobbyGameSettingUI : PopUpUI
    {
        // 배경음, 효과음, 마우스 감도

        [SerializeField] AudioMixer myMixer;   // 리소스로 가져오기

        private void Awake()
        {
            base.Awake();

            buttons["ReturnButton"].onClick.AddListener(() => { Return(); });
            buttons["ApplyButton"].onClick.AddListener(() => { Debug.Log("Apply"); });
            myMixer = GameManager.Resource.Load<AudioMixer>("MyMixer");
        }

        // Cancle을 위한 초기값들
        float initalMouseSensitivityValue;
        float initalBackgroundSoundValue;
        float initalEffectSoundValue;

        void OnEnable()
        {
            // 시작할 때의 기본 값들을 다 저장
            initalMouseSensitivityValue = sliders["MouseSensitivitySlider"].value;
            initalBackgroundSoundValue = sliders["BackgroundSoundSlider"].value;
            initalEffectSoundValue = sliders["EffectSoundSlider"].value;
        }

        public void MouseSensitivityControl()      // 마우스 감도
        {
            InGameSettingPopUpUI.OnMouseSensiticityControl?.Invoke(sliders["MouseSensitivitySlider"].value);
        }

        public void BackGroundSoundControl()       // BGM
        {
            float volume = sliders["BackgroundSoundSlider"].value;
            myMixer.SetFloat("BGM", volume);
        }

        public void EffectSoundControl()           // SFX
        {
            float volume = sliders["EffectSoundSlider"].value;
            myMixer.SetFloat("SFX", volume);
        }

        public void Return()     // 취소버튼
        {
            sliders["MouseSensitivitySlider"].value = initalMouseSensitivityValue;
            sliders["BackgroundSoundSlider"].value = initalBackgroundSoundValue;
            sliders["EffectSoundSlider"].value = initalEffectSoundValue;
            CloseUI();
        }

        public void Apply()     // 수락버튼
        {
            CloseUI();
        }
    }
}
