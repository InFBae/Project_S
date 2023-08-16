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
        // 배경음, 효과음, 감도, Cancle, 로비로 나가기, Confirm

        public static UnityEvent<float> OnMouseSensiticityControl = new UnityEvent<float>();
        public static UnityEvent OnPlayerInputActive = new UnityEvent();
        [SerializeField] AudioMixer myMixer;   // 리소스로 가져오기

        // Cancle을 위한 초기값들
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
            // 시작할 때의 기본 값들을 다 저장
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
            // 서버에서 내 플레이어만 찾아야 해서, 모든 플레이어를 순회하고 내 플레이어를 찾으면 그 플레이어에게만 반영되도록
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                    OnPlayerInputActive?.Invoke();      // Player의 InputSystem을 Active해주는 이벤트
                    CloseUI();
                }
            }
        }

        public void Cancle()
        {
            // STart()에서 기초 값들을 다 저장해놓고 그 기초값들로 다 바꿔줌

            // 서버에서 내 플레이어만 찾아야 해서, 모든 플레이어를 순회하고 내 플레이어를 찾으면 그 플레이어에게만 반영되도록
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    sliders["MouseSensitivitySlider"].value = initalMouseSensitivityValue;
                    sliders["BackgroundSoundSlider"].value = initalBackgroundSoundValue;
                    sliders["EffectSoundSlider"].value = initalEffectSoundValue;
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                    OnPlayerInputActive?.Invoke();      // Player의 InputSystem을 Active해주는 이벤트
                    CloseUI();
                }
            }
        }

        public void BackToLobby()   // 로비로 돌아가는 버튼
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