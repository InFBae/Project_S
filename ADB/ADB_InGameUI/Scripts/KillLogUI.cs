using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using JBB;

namespace ahndabi
{
    public class KillLogUI : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            KillManager.OnKilled.AddListener(CreateKillLog);
        }

        public void CreateKillLog(Photon.Realtime.Player killed, Photon.Realtime.Player dead, bool isHeadShot)
        {
            // TODO : Instantiate 로 UI 생성해주기. 3초 정도 동안

            texts["HeadShot"].text = isHeadShot ? "[Head Shot]" : "";
            texts["Nickname"].text = killed.GetNickname();
        }
    }
}