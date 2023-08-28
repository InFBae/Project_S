using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ahndabi
{
    public class ShowInGameSettingPopUpUI : MonoBehaviour
    {
        private void OnEnable()
        {
            InGameSettingPopUpUI.OnPlayerInputActive.AddListener(() => { PlayerInputActive();});
        }

        public void SettingPopUpUI()
        {
            GameManager.UI.ShowPopUpUI<InGameSettingPopUpUI>("UI/InGameSettingPopUpUI");
            Time.timeScale = 1f;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponent<PlayerInput>().enabled = false;     // Fire 되면 안됨. PlayerInput을 비활성화
            
        }

        void OnPopUpUI(InputValue value)
        {
            // ESC키를 누르면 세팅팝업UI가 떠야함
            // 서버에서 내 플레이어만 찾아야 해서, 모든 플레이어를 순회하고 내 플레이어를 찾으면 그 플레이어에게만 반영되도록
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    SettingPopUpUI();
                }
            }
        }

        public void PlayerInputActive()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsLocal)
                {
                    gameObject.GetComponent<PlayerInput>().enabled = true;
                }
            }
        }
    }
}