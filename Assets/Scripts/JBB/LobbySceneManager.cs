using MySql.Data.MySqlClient;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class LobbySceneManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] LobbyUI lobbyUI;

        public override void OnEnable()
        {
            base.OnEnable();
            GameManager.Instance.SceneLoadInit();
        }

        public override void OnDisable()
        {
            base.OnDisable();

        }

        public void Start()
        {
            string nick = NickNameChecking();
            if (nick == null || nick == "")
            {
                // �г��� ����
                lobbyUI.EnableNicknameUI();
            }
            GameManager.Chat.Connect(nick);
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Joined Lobby");
        }

        public string NickNameChecking()
        {
            string id = PhotonNetwork.LocalPlayer.NickName;
            string sqlCommand = $"SELECT U_Nickname FROM user_info WHERE U_ID='{id}'";
            MySqlDataReader reader = null;
            reader = GameManager.DB.Execute(sqlCommand);

            if (reader.HasRows)
            {
                reader.Read();
                string readNick = reader["U_Nickname"].ToString();
                return readNick;
            }
            return null;
        }

        /*
        public void LogOut()
        {
            GameManager.Scene.LoadScene("GameStartScene_");
            PhotonNetwork.LeaveLobby();
        }

        //�游��� ���������� ?
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            // �游��Ⱑ �����Ұ�� menuȭ�� ���ư�����


            // � ������ �����ߴ��� �α� �����
            Debug.Log($"create room failed with error({returnCode}) : {message}");
            //statePanel.AddMessage($"create room failed with error({returnCode}) : {message}");
        }
        */
    }
}

