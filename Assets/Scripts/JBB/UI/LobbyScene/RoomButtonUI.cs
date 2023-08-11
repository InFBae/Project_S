using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JBB
{
    public class RoomButtonUI : BaseUI
    {
        RoomInfo roomInfo;
        [SerializeField] Button roomButton;
        protected override void Awake()
        {
            base.Awake();

            roomButton.onClick.AddListener(OnRoomButtonClicked);
        }

        public void SetRoomInfo(RoomInfo roomInfo)
        {
            this.roomInfo = roomInfo;

            // TODO : 비밀번호 방 기능 구현
            images["UnLocked"].gameObject.SetActive(true);
            images["Locked"].gameObject.SetActive(false);

            texts["RoomName"].text = roomInfo.Name;            
            if (roomInfo.CustomProperties.ContainsKey("GameMode"))
            {
                texts["GameMode"].text = roomInfo.CustomProperties["GameMode"].ToString();
            }
            
            texts["Slots"].text = $"{roomInfo.PlayerCount} / {roomInfo.MaxPlayers}";

            if (roomInfo.CustomProperties.ContainsKey("IsPlaying"))
            {
                string state = (bool)roomInfo.CustomProperties["IsPlaying"] ? "PLAYING" : "";
                texts["State"].text = state;
            }
        }

        private void OnRoomButtonClicked()
        {
            if (roomInfo.PlayerCount < roomInfo.MaxPlayers)
            {
                PhotonNetwork.JoinRoom(roomInfo.Name);
            }
        }
    } 
}

