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
            gameObject.GetComponent<PlayerInput>().enabled = false;     // Fire �Ǹ� �ȵ�. PlayerInput�� ��Ȱ��ȭ
            
        }

        void OnPopUpUI(InputValue value)
        {
            // ESCŰ�� ������ �����˾�UI�� ������
            // �������� �� �÷��̾ ã�ƾ� �ؼ�, ��� �÷��̾ ��ȸ�ϰ� �� �÷��̾ ã���� �� �÷��̾�Ը� �ݿ��ǵ���
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