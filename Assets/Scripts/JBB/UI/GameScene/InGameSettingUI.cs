using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace JBB
{
    public class InGameSettingUI : BaseUI
    {
        // �����, ȿ����, ����, Cancle, �κ�� ������, Confirm

        public static UnityEvent<float> OnMouseSensitivityChanged = new UnityEvent<float>();
        public static UnityEvent OnPlayerInputActive = new UnityEvent();
        [SerializeField] AudioMixer myMixer;   // ���ҽ��� ��������
        [SerializeField] PlayerInput playerInput;

        [SerializeField] Slider backgroundSoundSlider;
        [SerializeField] Slider effectSoundSlider;
        [SerializeField] Slider mouseSensitivitySlider;

        PhotonView myPV;

        // Cancle�� ���� �ʱⰪ��
        float initalMouseSensitivityValue;
        float initalBackgroundSoundValue;
        float initalEffectSoundValue;

        protected override void Awake()
        {
            base.Awake();
            backgroundSoundSlider = sliders["BackgroundSoundSlider"];
            effectSoundSlider = sliders["EffectSoundSlider"];
            mouseSensitivitySlider = sliders["MouseSensitivitySlider"];
            buttons["ApplyButton"].onClick.AddListener(Apply);
            buttons["CancelButton"].onClick.AddListener(Cancel);
            buttons["BackToLobbyButton"].onClick.AddListener(BackToLobby);
            myMixer = GameManager.Resource.Load<AudioMixer>("Sound/MyMixer");

        }

        void OnEnable()
        {
            // ������ ���� �⺻ ������ �� ����
            initalMouseSensitivityValue = mouseSensitivitySlider.value;
            initalBackgroundSoundValue = backgroundSoundSlider.value;
            initalEffectSoundValue = effectSoundSlider.value;

            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;


            if (myPV == null)
            {
                PhotonView[] pvList = FindObjectsOfType<PhotonView>();

                foreach (PhotonView pv in pvList)
                {
                    if (pv.IsMine)
                    {
                        myPV = pv;
                        myPV.gameObject.GetComponent<PlayerInput>().enabled = false;
                    }
                }
            }
        }

        public void BackGroundSoundControl()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    float volume = backgroundSoundSlider.value;
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
                    float volume = effectSoundSlider.value;
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
                    OnMouseSensitivityChanged?.Invoke(mouseSensitivitySlider.value);
                }
            }
        }

        public void Apply()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            myPV.gameObject.GetComponent<PlayerInput>().enabled = true;

            gameObject.SetActive(false);
        }

        public void Cancel()
        {
            // STart()���� ���� ������ �� �����س��� �� ���ʰ���� �� �ٲ���

            mouseSensitivitySlider.value = initalMouseSensitivityValue;
            backgroundSoundSlider.value = initalBackgroundSoundValue;
            effectSoundSlider.value = initalEffectSoundValue;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            myPV.gameObject.GetComponent<PlayerInput>().enabled = true;

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

