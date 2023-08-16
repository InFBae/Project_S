using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
        [SerializeField] AudioMixer myMixer;   // ���ҽ��� ��������

        // Cancle�� ���� �ʱⰪ��
        float initalMouseSensitivityValue;
        float initalBackgroundSoundValue;
        float initalEffectSoundValue;

        private void Awake()
        {
            base.Awake();
            buttons["ApplyButton"].onClick.AddListener(() => { Apply(); });
            buttons["CancleButton"].onClick.AddListener(() => { Cancle(); });
            myMixer = GameManager.Resource.Load<AudioMixer>("MyMixer");

            OnMouseSensiticityControl?.Invoke(sliders["MouseSensitivitySlider"].value);
        }

        void OnEnable()
        {
            // ������ ���� �⺻ ������ �� ����
            initalMouseSensitivityValue = sliders["MouseSensitivitySlider"].value;
            initalBackgroundSoundValue = sliders["BackgroundSoundSlider"].value;
            initalEffectSoundValue = sliders["EffectSoundSlider"].value;
        }

        public void BacckGroundSoundControl()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    float volume = sliders["BackgroundSoundSlider"].value;
                    myMixer.SetFloat("BGM", volume);
                }
            }
        }

        public void EffectSoundControl()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    float volume = sliders["EffectSoundSlider"].value;
                    myMixer.SetFloat("SFX", volume);
                }
            }
        }

        public void MouseSensitivityControl()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    OnMouseSensiticityControl?.Invoke(sliders["MouseSensitivitySlider"].value);
                }
            }
        }

        public void Apply()
        {
            // �������� �� �÷��̾ ã�ƾ� �ؼ�, ��� �÷��̾ ��ȸ�ϰ� �� �÷��̾ ã���� �� �÷��̾�Ը� �ݿ��ǵ���
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                    OnPlayerInputActive?.Invoke();      // Player�� InputSystem�� Active���ִ� �̺�Ʈ
                    CloseUI();
                }
            }
        }

        public void Cancle()
        {
            // STart()���� ���� ������ �� �����س��� �� ���ʰ���� �� �ٲ���

            // �������� �� �÷��̾ ã�ƾ� �ؼ�, ��� �÷��̾ ��ȸ�ϰ� �� �÷��̾ ã���� �� �÷��̾�Ը� �ݿ��ǵ���
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    sliders["MouseSensitivitySlider"].value = initalMouseSensitivityValue;
                    sliders["BackgroundSoundSlider"].value = initalBackgroundSoundValue;
                    sliders["EffectSoundSlider"].value = initalEffectSoundValue;
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                    OnPlayerInputActive?.Invoke();      // Player�� InputSystem�� Active���ִ� �̺�Ʈ
                    CloseUI();
                }
            }
        }

        public void BackToLobby()   // �κ�� ���ư��� ��ư
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    PhotonNetwork.LoadLevel("LobbyScene");
                }
            }
        }
    }
}