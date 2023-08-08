using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class LobbyUI : BaseUI
    {
        NicknameUI nicknameUI;
        FriendListUI friendListUI;
        protected override void Awake()
        {
            base.Awake();

            nicknameUI = GetComponentInChildren<NicknameUI>();
            nicknameUI.gameObject.SetActive(false);
            friendListUI = GetComponentInChildren<FriendListUI>();
        }

        public void EnableNicknameUI()
        {
            nicknameUI.gameObject.SetActive(true);
        }

    }
}

