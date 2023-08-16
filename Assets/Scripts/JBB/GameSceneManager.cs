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
    public class GameSceneManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] JBB.InGameUI inGameUI;
        [SerializeField] GameObject spawnPointsPrefab;

        Transform[] spawnPoints;

        public static UnityEvent<Player, Player, bool> OnKilled = new UnityEvent<Player, Player, bool>();

        private void Awake()
        {
            spawnPoints = spawnPointsPrefab.GetComponentsInChildren<Transform>();
        }

        private void Start()
        {          
            if (PhotonNetwork.InRoom)
            {
                inGameUI.InitUI();
                Transform spawnPoint = GetSpawnPoint();
                PhotonNetwork.Instantiate("AllInOnePlayerTest", spawnPoint.position, Quaternion.identity);
                // 게임 준비사항 다 마치고 SetLoad 설정
                PhotonNetwork.LocalPlayer.SetLoad(true);
            }
            else
            {
                // DebugMode
                PhotonNetwork.LocalPlayer.NickName = $"Debug {UnityEngine.Random.Range(100, 200)}";
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

            PhotonNetwork.LocalPlayer.SetNickname($"Debug {UnityEngine.Random.Range(100, 200)}");
            PhotonNetwork.LocalPlayer.SetLoad(true);

            inGameUI.InitUI();

            // TODO : Player 한명만 소환으로 수정
            //for (int i = 0; i < 7; i++)
            //{
                Transform spawnPoint = GetSpawnPoint();
                PhotonNetwork.Instantiate("AllInOnePlayerTest", spawnPoint.position, Quaternion.identity);
            //}
            
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
                inGameUI.InitUI();
            }  
            if (changedProps.ContainsKey("KillCount"))
            {
                if (targetPlayer == PhotonNetwork.LocalPlayer)
                {
                    inGameUI.UpdateKillDeathUI();
                }
                inGameUI.UpdateRankingBoard();
                inGameUI.UpdateTargetKillSliderValue();
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
            double gameEndTime = (PhotonNetwork.CurrentRoom.GetLoadTime() + PhotonNetwork.CurrentRoom.GetGameTime() * 60);
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

        public Transform GetSpawnPoint()
        {
            Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, 8)];
            while (Physics.Raycast(spawnPoint.position, Vector3.up, 2))
            {
                spawnPoint = spawnPoints[UnityEngine.Random.Range(0, 8)];
            }
            return spawnPoint;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            
        }
    }
}

