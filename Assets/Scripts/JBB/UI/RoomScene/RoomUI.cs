using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JBB
{
    public class RoomUI : BaseUI
    {
        RoomInfoUI roomInfoUI;
        PlayerListUI playerListUI;
        FriendListUI friendListUI;
        RoomSettingUI roomSettingUI;
        protected override void Awake()
        {
            base.Awake();

            buttons["GameReadyButton"].onClick.AddListener(OnGameReadyButtonClicked);
            buttons["GameStartButton"].onClick.AddListener(OnGameStartButtonClicked);
            buttons["InviteFriendButton"].onClick.AddListener(OnInviteFriendButtonClicked);
            buttons["RoomSettingButton"].onClick.AddListener(OnRoomSettingButtonClicked);
            buttons["ExitButton"].onClick.AddListener(OnExitButtonClicked);

            roomInfoUI = GetComponentInChildren<RoomInfoUI>();
            playerListUI = GetComponentInChildren<PlayerListUI>();
            friendListUI = GetComponentInChildren<FriendListUI>();

            roomSettingUI = GetComponentInChildren<RoomSettingUI>();
            roomSettingUI.gameObject.SetActive(false);
        }

        private void OnGameReadyButtonClicked()
        {
            PhotonNetwork.LocalPlayer.SetReady(!PhotonNetwork.LocalPlayer.GetReady());
        }
        private void OnGameStartButtonClicked()
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
        private void OnInviteFriendButtonClicked()
        {

        }
        private void OnRoomSettingButtonClicked()
        {
            roomSettingUI.gameObject.SetActive(true);
        }
        private void OnExitButtonClicked()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void UpdateRoomInfo()
        {
            roomInfoUI.UpdateRoomInfo();
        }

        public void UpdateFriendList()
        {
            friendListUI.UpdateFriendList();
        }

        public void UpdatePlayerList()
        {
            playerListUI.UpdatePlayerList();
        }

        public void UpdatePlayerSlot(Player player)
        {
            playerListUI.UpdatePlayerSlot(player);
        }
    }
}
