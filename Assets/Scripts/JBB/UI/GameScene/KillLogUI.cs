using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JBB;


namespace JBB
{
    public class KillLogUI : BaseUI
    {
        [SerializeField] GameObject content;

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
            
        }
    }
}

