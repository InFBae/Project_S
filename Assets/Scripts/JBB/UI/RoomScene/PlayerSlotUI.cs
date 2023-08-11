using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    
    public class PlayerSlotUI : BaseUI
    {
        Player player;

        protected override void Awake()
        {
            base.Awake();
        }

        public void SetPlayerSlot(Player player)
        {
            this.player = player;
            images["Host"].enabled = player.IsMasterClient;
            texts["Nickname"].text = player.GetNickname();            
            texts["State"].text = player.GetReady() ? "READY" : "";
        }

        public Player GetPlayer()
        {
            return player;
        }
    }
}

