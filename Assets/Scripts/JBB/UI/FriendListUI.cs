using MySql.Data.MySqlClient;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class FriendListUI : BaseUI
    {
        [SerializeField] GameObject content;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            ChatManager.OnFriedntStatusChanged.AddListener(UpdateFriendStatus);
        }

        private void OnDisable()
        {
            ChatManager.OnFriedntStatusChanged.RemoveListener(UpdateFriendStatus);
        }

        private void Start()
        {
            UpdateFriendList();
        }

        public void UpdateFriendList()
        {
            string sqlCommand = $"SELECT * FROM friend_info WHERE Owner = '{PhotonNetwork.LocalPlayer.NickName}'";
            MySqlDataReader reader = null ;
            reader = GameManager.DB.Execute(sqlCommand);
            
            if (reader.HasRows)
            {
                reader.Read();
                for (int i = 0; i < 10; i++)
                {
                    string userId = reader[$"friend{i+1}"].ToString();
                    // TODO : ÇÁ·»µå½½·Ô Ãß°¡
                    if (userId != "")
                    {
                        FriendSlotUI friendSlot = GameManager.Pool.GetUI(GameManager.Resource.Load<FriendSlotUI>("UI/FriendSlot"));
                        string sqlCommand2 = $"SELECT U_Nick FROM user_info WHERE U_ID = '{userId}'";
                        MySqlDataReader nicknameReader = null;
                        nicknameReader = GameManager.DB.Execute(sqlCommand2);
                        if (nicknameReader.HasRows)
                        {
                            nicknameReader.Read();
                            string nickname = nicknameReader["U_Nick"].ToString();
                            friendSlot.SetState(nickname, 2);
                            friendSlot.transform.SetParent(content.transform, false);
                        }
                        else
                        {
                            Debug.Log("Nickname query Failed");
                        }
                    }
                    Debug.Log(i+1 + userId);
                }
            }            
        }


        public void UpdateFriendStatus(string nickname, int state)
        {
            FriendSlotUI[] friendSlots = GetComponentsInChildren<FriendSlotUI>();
            foreach (FriendSlotUI friendSlot in friendSlots)
            {
                if (friendSlot.GetNickname() == nickname)
                {
                    friendSlot.SetState(nickname, state);
                }
            }
        }
    }
}

