using MySql.Data.MySqlClient;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JBB
{
    public class FindFriendUI : BaseUI
    {
        [SerializeField] TMP_InputField nicknameInput;
        protected override void Awake()
        {
            base.Awake();

            buttons["ReturnButton"].onClick.AddListener(OnReturnButtonClicked);
            buttons["FindButton"].onClick.AddListener(OnFindButtonClicked);
        }

        public void OnReturnButtonClicked()
        {
            this.gameObject.SetActive(false);
        }

        public void OnFindButtonClicked()
        {
            string nickname = nicknameInput.text;
            string mysqlCommand = $"SELECT * FROM user_info WHERE U_Nickname = '{nickname}'";
            MySqlDataReader reader = null;
            reader = GameManager.DB.Execute(mysqlCommand);

            // nickname이 DB에 존재하는지 검사           
            if (reader.HasRows)
            {
                reader.Read();
                if (reader["U_ID"].ToString() == PhotonNetwork.LocalPlayer.NickName)
                {
                    GameManager.UI.CreatePopUpMessage("Add Friend Failed(Can't Add Self)");
                    return;
                }

                string getFriendListCommand = $"SELECT * FROM friend_info WHERE Owner = '{PhotonNetwork.LocalPlayer.NickName}'";
                reader = GameManager.DB.Execute(getFriendListCommand);
                if (reader.HasRows)
                {
                    reader.Read();
                    for(int i = 1; i < 11; i++)
                    {
                        string friend = reader[$"Friend{i}"].ToString();
                        if (friend != "")
                        {
                            if (friend == nickname)
                            {
                                GameManager.UI.CreatePopUpMessage("Add Friend Failed(Already Friend)");
                                return;
                            }
                            continue;
                        }
                        string updateFriendInfoCommand = $"UPDATE friend_info SET Friend{i} = '{nickname}' WHERE Owner = '{PhotonNetwork.LocalPlayer.NickName}'";
                        GameManager.DB.ExecuteNonQuery(updateFriendInfoCommand);
                        GameManager.UI.CreatePopUpMessage("Add Friend Success");
                        ChatManager.OnFriendListChanged?.Invoke();
                        return;
                    }
                    GameManager.UI.CreatePopUpMessage("Add Friend Failed(Full FriendList)");
                    return;
                }
                else
                {
                    // DB에 이 유저의 friend_info 정보가 없으므로 생성
                    string addFriendInfoCommand = $"INSERT INTO friend_info (Owner, Friend1) values ('{PhotonNetwork.LocalPlayer.NickName}', '{nickname}')";
                    GameManager.DB.ExecuteNonQuery(addFriendInfoCommand);
                    GameManager.UI.CreatePopUpMessage("Add Friend Success");
                    ChatManager.OnFriendListChanged?.Invoke();
                    return;
                }
            }
            else
            {
                GameManager.UI.CreatePopUpMessage("Nickname Not In Database");
                return;
            }
        }
    }
}

