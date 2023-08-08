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
                // 닉네임 생성
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

        //방만들기 실패했을때 ?
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            // 방만들기가 실패할경우 menu화면 돌아가야함


            // 어떤 이유로 실패했는지 로그 찍어줌
            Debug.Log($"create room failed with error({returnCode}) : {message}");
            //statePanel.AddMessage($"create room failed with error({returnCode}) : {message}");
        }
        */
    }
}

