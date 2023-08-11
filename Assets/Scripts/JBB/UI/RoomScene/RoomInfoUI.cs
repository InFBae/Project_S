using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JBB
{
    public class RoomInfoUI : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void UpdateRoomInfo()
        {
            texts["RoomNameText"].text = PhotonNetwork.CurrentRoom.Name;
            texts["CurrentPlayerText"].text = $"{PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}";
            texts["GameTypeText"].text = $"{PhotonNetwork.CurrentRoom.GetGameType()}";
            texts["GameTimeText"].text = $"{PhotonNetwork.CurrentRoom.GetGameTime()} min";
            texts["MaxKillText"].text = $"{PhotonNetwork.CurrentRoom.GetMaxKill()}";
            texts["IntrusionText"].text = PhotonNetwork.CurrentRoom.GetIntrusion() ? "ON" : "OFF";
        }
    }
}

