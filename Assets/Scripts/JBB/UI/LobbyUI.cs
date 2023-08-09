using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class LobbyUI : BaseUI
    {
        NicknameUI nicknameUI;
        FindFriendUI findFriendUI;
        protected override void Awake()
        {
            base.Awake();

            nicknameUI = GetComponentInChildren<NicknameUI>();
            nicknameUI.gameObject.SetActive(false);
            findFriendUI = GetComponentInChildren<FindFriendUI>();
            findFriendUI.gameObject.SetActive(false);

            buttons["FindFriendButton"].onClick.AddListener(OnFindFriendButtonClicked);
            buttons["LogOutButton"].onClick.AddListener(OnLogOutButtonClicked);
        }

        public void EnableNicknameUI()
        {
            nicknameUI.gameObject.SetActive(true);
        }

        public void OnFindFriendButtonClicked()
        {
            findFriendUI.gameObject.SetActive(true);
        }

        public void OnLogOutButtonClicked()
        {
            GameManager.Chat.DisConnect();
            PhotonNetwork.Disconnect();
            GameManager.Scene.LoadScene("GameStartScene");           
        }
    }
}

