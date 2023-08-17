using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JBB;

namespace ahndabi
{
    public class ADB_LobbyUI : BaseUI
    {
        NicknameUI nicknameUI;
        MakeRoomUI makeRoomUI;
        FindFriendUI findFriendUI;
        RoomListUI roomListUI;
        protected override void Awake()
        {
            base.Awake();

            nicknameUI = GetComponentInChildren<NicknameUI>();
            nicknameUI.gameObject.SetActive(false);
            makeRoomUI = GetComponentInChildren<MakeRoomUI>();
            makeRoomUI.gameObject.SetActive(false);
            findFriendUI = GetComponentInChildren<FindFriendUI>();
            findFriendUI.gameObject.SetActive(false);
            roomListUI = GetComponentInChildren<RoomListUI>();

            buttons["MakeRoomButton"].onClick.AddListener(OnMakeRoomButtonClicked);
            buttons["QuickMatchButton"].onClick.AddListener(OnQuickMatchButtonClicked);
            buttons["SettingsButton"].onClick.AddListener(OnSettingsButtonClicked);
            buttons["FindFriendButton"].onClick.AddListener(OnFindFriendButtonClicked);
            buttons["LogOutButton"].onClick.AddListener(OnLogOutButtonClicked);
        }

        public void EnableNicknameUI()
        {
            nicknameUI.gameObject.SetActive(true);
        }

        public void OnMakeRoomButtonClicked()
        {
            makeRoomUI.gameObject.SetActive(true);
        }

        public void OnQuickMatchButtonClicked()
        {
            PhotonNetwork.JoinRandomRoom();
            // TODO : 방을 생성도 할 것인지, 실패했을 때 OnJoinRandomFailed 함수 작성
        }

        public void OnSettingsButtonClicked()
        {
            GameManager.UI.ShowPopUpUI<LobbyGameSettingUI>("UI/LobbyGameSettingUI");
        }
        public void OnFindFriendButtonClicked()
        {
            findFriendUI.gameObject.SetActive(true);
        }

        public void OnLogOutButtonClicked()
        {
            GameManager.Chat.DisConnect();
            PhotonNetwork.Disconnect();
            GameManager.Scene.LoadScene("GameStartScene");
        }

        public void UpdateRoomList(List<RoomInfo> roomList)
        {
            roomListUI.UpdateRoomList(roomList);
        }
    }
}