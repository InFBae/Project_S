using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace JBB
{
    public class InGameSettingUI : BaseUI
    {
        // �����, ȿ����, ����, Cancle, �κ�� ������, Confirm

        public static UnityEvent<float> OnMouseSensiticityControl = new UnityEvent<float>();
        public static UnityEvent OnPlayerInputActive = new UnityEvent();
        AudioMixer myMixer;   // ���ҽ��� ��������
        [SerializeField] PlayerInput playerInput;

        // Cancle�� ���� �ʱⰪ��
        float initalMouseSensitivityValue;
        float initalBackgroundSoundValue;
        float initalEffectSoundValue;

        protected override void Awake()
        {
            base.Awake();
            buttons["ApplyButton"].onClick.AddListener(Apply);
            buttons["CancelButton"].onClick.AddListener(Cancel);
            buttons["BackToLobbyButton"].onClick.AddListener(BackToLobby);
            myMixer = GameManager.Resource.Load<AudioMixer>("Sound/MyMixer");

        }

        void OnEnable()
        {
            // ������ ���� �⺻ ������ �� ����
            initalMouseSensitivityValue = sliders["MouseSensitivitySlider"].value;
            initalBackgroundSoundValue = sliders["BackgroundSoundSlider"].value;
            initalEffectSoundValue = sliders["EffectSoundSlider"].value;

            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        public void BackGroundSoundControl()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    float volume = sliders["BackgroundSoundSlider"].value;
                    myMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
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
                    //myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
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
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            OnPlayerInputActive?.Invoke();      // Player�� InputSystem�� Active���ִ� �̺�Ʈ
            gameObject.SetActive(false);
        }

        public void Cancel()
        {
            // STart()���� ���� ������ �� �����س��� �� ���ʰ���� �� �ٲ���

            sliders["MouseSensitivitySlider"].value = initalMouseSensitivityValue;
            sliders["BackgroundSoundSlider"].value = initalBackgroundSoundValue;
            sliders["EffectSoundSlider"].value = initalEffectSoundValue;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            OnPlayerInputActive?.Invoke();      // Player�� InputSystem�� Active���ִ� �̺�Ʈ
            gameObject.SetActive(false);
        }

        public void BackToLobby()   // �κ�� ���ư��� ��ư
        {
            if(PhotonNetwork.IsMasterClient)
            {
                if(PhotonNetwork.PlayerListOthers.Length > 0)
                {
                    Photon.Realtime.Player player = PhotonNetwork.PlayerListOthers[0];
                    PhotonNetwork.SetMasterClient(player);
                }
                GameManager.UI.CreatePopUpMessage("Changing Host", 1f);
            }
            else
            {
                PhotonNetwork.LeaveRoom();
            }           
        }
    }
}

