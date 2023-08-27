using MySql.Data.MySqlClient;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JBB
{
    public class NicknameUI : BaseUI
    {
        [SerializeField] TMP_InputField nicknameInput;
        protected override void Awake()
        {
            base.Awake();

            buttons["ConfirmButton"].onClick.AddListener(OnConfirmButtonClicked);
        }

        private void OnConfirmButtonClicked()
        {
            string id = PhotonNetwork.LocalPlayer.NickName;
            string nick = nicknameInput.text;

            if (nick == "")
            {
                GameManager.UI.CreatePopUpMessage("Input Nickname", 1f);
                Debug.Log("Input Nickname");
                return;
            }

            string sqlCommand = $"SELECT U_ID FROM user_info WHERE U_Nickname = '{nick}'";
            MySqlDataReader reader = null;
            reader = GameManager.DB.Execute(sqlCommand);
            
            if (reader.HasRows)
            {
                GameManager.UI.CreatePopUpMessage("Same Nickname exists", 1f);
                Debug.Log("Same Nickname exists");
            }
            else
            {          
                sqlCommand = $"UPDATE user_info SET U_Nickname='{nick}' WHERE U_ID = '{id}'";
                GameManager.DB.ExecuteNonQuery(sqlCommand);

                GameManager.UI.CreatePopUpMessage("Nickname Create Success", 1f);
                GameManager.Chat.Connect(nick);
                this.gameObject.SetActive(false);
            }
        }
    }
}

