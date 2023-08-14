using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class KillDeathUI : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void UpdateKillDeathText()
        {
            texts["KillDeathText"].text = $"{PhotonNetwork.LocalPlayer.GetKillCount()} / {PhotonNetwork.LocalPlayer.GetDeathCount()}";
        }
    }
}

