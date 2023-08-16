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
            SettingPopUpUI();
        }

        public void PlayerInputActive()
        {
            gameObject.GetComponent<PlayerInput>().enabled = true;
        }
    }
}