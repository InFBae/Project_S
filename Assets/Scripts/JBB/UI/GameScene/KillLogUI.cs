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
        public GameObject content;
        public PhotonView myPV;

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
            if (myPV == null)
            {
                PhotonView[] pvList = FindObjectsOfType<PhotonView>();
                foreach (PhotonView pv in pvList)
                {
                    if (pv.IsMine)
                    {
                        myPV = pv;
                    }
                }
            }
            Debug.Log($"Create KillLog {killed.GetNickname()} killed {dead.GetNickname()}");
            if (killed == PhotonNetwork.LocalPlayer)
            {
                myPV.RPC("CreateKillLog", RpcTarget.All, isHeadShot, killed);
            }            
        }
    }
}

