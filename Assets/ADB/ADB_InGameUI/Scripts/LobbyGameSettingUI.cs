using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

namespace ahndabi
{
    public class LobbyGameSettingUI : BaseUI
    {
        // �����, ȿ����, ���콺 ����

        [SerializeField] AudioMixer myMixer;   // ���ҽ��� ��������

        protected override void Awake()
        {
            base.Awake();

            buttons["ReturnButton"].onClick.AddListener(() => { Return(); });
            buttons["ApplyButton"].onClick.AddListener(() => { Apply(); });
            myMixer = GameManager.Resource.Load<AudioMixer>("Sound/MyMixer");
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
            JBB.InGameSettingUI.OnMouseSensitivityChanged?.Invoke(sliders["MouseSensitivitySlider"].value);
        }

        public void BackGroundSoundControl()       // BGM
        {
            float volume = sliders["BackgroundSoundSlider"].value;
            myMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        }

        public void EffectSoundControl()           // SFX
        {
            float volume = sliders["EffectSoundSlider"].value;
            myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }

        public void Return()     // ��ҹ�ư
        {
            sliders["MouseSensitivitySlider"].value = initalMouseSensitivityValue;
            sliders["BackgroundSoundSlider"].value = initalBackgroundSoundValue;
            sliders["EffectSoundSlider"].value = initalEffectSoundValue;
            gameObject.SetActive(false);
        }

        public void Apply()     // ������ư
        {
            gameObject.SetActive(false);
        }
    }
}
