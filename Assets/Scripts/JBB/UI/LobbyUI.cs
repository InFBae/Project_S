using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class LobbyUI : BaseUI
    {
        NicknameUI nicknameUI;
        protected override void Awake()
        {
            base.Awake();

            nicknameUI = GetComponentInChildren<NicknameUI>();
            nicknameUI.gameObject.SetActive(false);
        }

        public void EnableNicknameUI()
        {
            nicknameUI.gameObject.SetActive(true);
        }
    }
}

