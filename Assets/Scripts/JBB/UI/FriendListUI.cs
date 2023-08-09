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
            ChatManager.OnFriendStatusChanged.AddListener(UpdateFriendStatus);
            ChatManager.OnFriendListChanged.AddListener(UpdateFriendList);
        }

        private void OnDisable()
        {
            ChatManager.OnFriendStatusChanged.RemoveListener(UpdateFriendStatus);
            ChatManager.OnFriendListChanged.RemoveListener(UpdateFriendList);
        }

        private void Start()
        {
            UpdateFriendList();
        }

        public void UpdateFriendList()
        {
            // TODO 리스트 안에 객체들이 있을 때 비우고 재생성해야 함
            string sqlCommand = $"SELECT * FROM friend_info WHERE Owner = '{PhotonNetwork.LocalPlayer.NickName}'";
            MySqlDataReader reader = null ;
            reader = GameManager.DB.Execute(sqlCommand);
            
            if (reader.HasRows)
            {
                reader.Read();
                for (int i = 0; i < 10; i++)
                {
                    string nickname = reader[$"friend{i+1}"].ToString();
                    // TODO : 프렌드슬롯 추가
                    if (nickname != "")
                    {
                        FriendSlotUI friendSlot = GameManager.Pool.GetUI(GameManager.Resource.Load<FriendSlotUI>("UI/FriendSlot"));

                        friendSlot.SetState(nickname, 0);
                        friendSlot.transform.SetParent(content.transform, false);
                    }
                    Debug.Log(i+1 + nickname);
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

