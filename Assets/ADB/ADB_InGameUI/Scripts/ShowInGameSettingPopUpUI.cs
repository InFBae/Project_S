using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ahndabi
{
    public class ShowInGameSettingPopUpUI : MonoBehaviour
    {
        public void SettingPopUpUI()
        {
            GameManager.UI.ShowPopUpUI<InGameSettingPopUpUI>("UI/InGameSettingPopUpUI");
            Time.timeScale = 1f;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponent<PlayerInput>().enabled = false;     // Fire �Ǹ� �ȵ�. PlayerInput�� ��Ȱ��ȭ
        }

        void OnPopUpUI(InputValue value)
        {
            // ESCŰ�� ������ �����˾�UI�� ������
            SettingPopUpUI();
        }
    }
}