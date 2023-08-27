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
        // 배경음, 효과음, 감도, Cancle, 로비로 나가기, Confirm

        public static UnityEvent<float> OnMouseSensiticityControl = new UnityEvent<float>();
        public static UnityEvent OnPlayerInputActive = new UnityEvent();
        AudioMixer myMixer;   // 리소스로 가져오기
        [SerializeField] PlayerInput playerInput;

        // Cancle을 위한 초기값들
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
            // 시작할 때의 기본 값들을 다 저장
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
            OnPlayerInputActive?.Invoke();      // Player의 InputSystem을 Active해주는 이벤트
            gameObject.SetActive(false);
        }

        public void Cancel()
        {
            // STart()에서 기초 값들을 다 저장해놓고 그 기초값들로 다 바꿔줌

            sliders["MouseSensitivitySlider"].value = initalMouseSensitivityValue;
            sliders["BackgroundSoundSlider"].value = initalBackgroundSoundValue;
            sliders["EffectSoundSlider"].value = initalEffectSoundValue;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            OnPlayerInputActive?.Invoke();      // Player의 InputSystem을 Active해주는 이벤트
            gameObject.SetActive(false);
        }

        public void BackToLobby()   // 로비로 돌아가는 버튼
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

