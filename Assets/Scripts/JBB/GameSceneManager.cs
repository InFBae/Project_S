using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

namespace JBB
{
    public class GameSceneManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] InGameUI inGameUI;

        public static UnityEvent<Player, Player, bool> OnKilled = new UnityEvent<Player, Player, bool>();
        private void Start()
        {          
            if (PhotonNetwork.InRoom)
            {
                inGameUI.InitUI();
            }
            else
            {
                // DebugMode
                PhotonNetwork.LocalPlayer.NickName = "111";
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            RoomOptions roomOptions = new RoomOptions() { IsVisible = false, IsOpen = true, MaxPlayers = 8 };
            PhotonHashtable RoomCustomProps = new PhotonHashtable()
            {
                {"IsPlaying" , false},
                { "GameType", "SOLO"},
                {"GameTime", 20 },
                {"MaxKill", 20 },
                {"Intrusion", false}
            };

            roomOptions.CustomRoomProperties = RoomCustomProps;
            PhotonNetwork.JoinOrCreateRoom("Debug", roomOptions, TypedLobby.Default);
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("Joined DebugRoom");
            PhotonNetwork.LeaveLobby();

            PhotonNetwork.LocalPlayer.SetNickname("111");
            PhotonNetwork.LocalPlayer.SetLoad(true);

            inGameUI.InitUI();
            GameStart();
        }


        public override void OnEnable()
        {
            base.OnEnable();
            GameManager.Instance.SceneLoadInit();

            OnKilled.AddListener(ChangeKillDeathProperty);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            OnKilled.RemoveListener(ChangeKillDeathProperty);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, PhotonHashtable changedProps)
        {
            if (changedProps.ContainsKey("LOAD"))
            {
                if (PlayerLoadCount() == PhotonNetwork.PlayerList.Length)
                {
                    if (PhotonNetwork.IsMasterClient)
                        PhotonNetwork.CurrentRoom.SetLoadTime(PhotonNetwork.Time);
                }
                else
                {
                    Debug.Log($"Wait players {PlayerLoadCount()} / {PhotonNetwork.PlayerList.Length}");
                }
            }

        }
        public override void OnRoomPropertiesUpdate(PhotonHashtable propertiesThatChanged)
        {
            if (propertiesThatChanged.ContainsKey("LoadTime"))
            {
                // 플레이어가 모두 로드되면 게임 시작
                GameStart();
            }
        }

        public void ChangeKillDeathProperty(Player killed, Player dead, bool isHeadShot)
        {
            killed.SetKillCount(killed.GetKillCount() + 1);
            dead.SetDeathCount(dead.GetDeathCount() + 1);
        }

        public void GameStart()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(TimerRoutine());
            }           
        }

        IEnumerator TimerRoutine()
        {
            float gameEndTime = (float)PhotonNetwork.Time + PhotonNetwork.CurrentRoom.GetGameTime() * 60;
            while (PhotonNetwork.Time < gameEndTime)
            {
                int remainTime = (int)(gameEndTime - PhotonNetwork.Time);
                TimeUI.OnLeftTimeChanged?.Invoke(remainTime);
                yield return new WaitForSeconds(1f);
            } 
            // 타이머 종료 > END GAME
        }

        private int PlayerLoadCount()
        {
            int loadCount = 0;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.GetLoad())
                    loadCount++;
            }
            return loadCount;
        }
    }
}

