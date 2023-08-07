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
        private MySqlConnection con;
        private MySqlDataReader reader;

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
            ConnectDataBase();

        }


        private void ConnectDataBase()
        {
            try
            {
                string serverInfo = "Server=43.200.178.18; Database=userdb; Uid=root; Pwd=1234; Port=3306; CharSet=utf8;";
                con = new MySqlConnection(serverInfo);
                con.Open();

                // 성공 확인
                Debug.Log("DataBase connect success");
            }
            catch (Exception e)
            {

                Debug.Log(e.Message);
            }
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Joined Lobby");
        }

    }
}

