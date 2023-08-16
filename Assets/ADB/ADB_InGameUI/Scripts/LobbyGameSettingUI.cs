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
        // �����, ȿ����, ���콺 ����

        [SerializeField] AudioMixer myMixer;   // ���ҽ��� ��������

        private void Awake()
        {
            base.Awake();

            buttons["ReturnButton"].onClick.AddListener(() => { Return(); });
            buttons["ApplyButton"].onClick.AddListener(() => { Debug.Log("Apply"); });
            myMixer = GameManager.Resource.Load<AudioMixer>("MyMixer");
        }

        // Cancle�� ���� �ʱⰪ��
        float initalMouseSensitivityValue;
        float initalBackgroundSoundValue;
        float initalEffectSoundValue;

        void OnEnable()
        {
            // ������ ���� �⺻ ������ �� ����
            initalMouseSensitivityValue = sliders["MouseSensitivitySlider"].value;
            initalBackgroundSoundValue = sliders["BackgroundSoundSlider"].value;
            initalEffectSoundValue = sliders["EffectSoundSlider"].value;
        }

        public void MouseSensitivityControl()      // ���콺 ����
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

        public void Return()     // ��ҹ�ư
        {
            sliders["MouseSensitivitySlider"].value = initalMouseSensitivityValue;
            sliders["BackgroundSoundSlider"].value = initalBackgroundSoundValue;
            sliders["EffectSoundSlider"].value = initalEffectSoundValue;
            CloseUI();
        }

        public void Apply()     // ������ư
        {
            CloseUI();
        }
    }
}
