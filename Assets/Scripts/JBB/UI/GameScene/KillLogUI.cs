using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JBB
{
    public class KillLogUI : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            GameSceneManager.OnKilled.AddListener(CreateKillLog);
        }

        public void CreateKillLog(Player killed, Player dead, bool isHeadShot)
        {
            Debug.Log($"Create KillLog {killed.GetNickname()} killed {dead.GetNickname()}");
            // TODO 킬로그 만들기
            //texts["HeadShot"].text = isHeadShot ? "[Head Shot]" : "";
            //texts["Nickname"].text = killed.GetNickname();
        }
    }
}

