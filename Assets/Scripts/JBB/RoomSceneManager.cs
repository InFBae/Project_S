using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JBB
{
    public class RoomSceneManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] RoomUI roomUI;

        public override void OnEnable()
        {
            base.OnEnable();
            GameManager.Instance.SceneLoadInit();
        }

        public override void OnDisable()
        {
            base.OnDisable();

        }
        private void Start()
        {
            // DebugMode
            if (!PhotonNetwork.InRoom)
            {
                PhotonNetwork.LocalPlayer.NickName = "111";                
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.JoinLobby();
                GameManager.Chat.Connect(PhotonNetwork.LocalPlayer.NickName);               
            }
            else
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                roomUI.UpdateRoomInfo();
                roomUI.UpdateFriendList();
                roomUI.UpdatePlayerList();
            }
        }

        public override void OnConnectedToMaster()
        {            
            //RoomOptions roomOptions = new RoomOptions() { IsVisible = false, IsOpen = true, MaxPlayers = 8 };

            //PhotonNetwork.JoinOrCreateRoom("Debug", roomOptions, TypedLobby.Default);           
        }
        public override void OnJoinedRoom()
        {                      
            Debug.Log("Joined DebugRoom");
            PhotonNetwork.LeaveLobby();

            PhotonNetwork.LocalPlayer.SetNickname(GameManager.Chat.Nickname);
            PhotonNetwork.LocalPlayer.SetReady(false);
            PhotonNetwork.LocalPlayer.SetLoad(false);
            
            roomUI.UpdateRoomInfo();
            roomUI.UpdateFriendList();
            roomUI.UpdatePlayerList();           
        }

        public override void OnLeftRoom()
        {
            GameManager.Scene.LoadScene("LobbyScene");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            roomUI.UpdatePlayerList();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            roomUI.UpdatePlayerList();
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            roomUI.UpdatePlayerSlot(targetPlayer);
            CheckReady();
        }

        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            roomUI.UpdateRoomInfo();
        }

        private void CheckReady()
        {
            int readyCount = 0;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if(player.GetReady())
                    readyCount++;
            }
            if (readyCount == PhotonNetwork.PlayerList.Length && readyCount > 1)
            {
                roomUI.ActivateGameStartButton();
            }
            else
            {
                roomUI.DeActivateGameStartButton();
            }
        }
    }
}

