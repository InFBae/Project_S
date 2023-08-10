using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

namespace JBB
{
    public class MakeRoomUI : BaseUI
    {
        [SerializeField] TMP_InputField roomNameInput;
        [SerializeField] TMP_Dropdown maxPlayerDropdown;
        [SerializeField] TMP_Dropdown gameTypeDropdown;
        [SerializeField] TMP_Dropdown gameTimeDropdown;
        [SerializeField] TMP_Dropdown maxKillDropdown;
        [SerializeField] Toggle intrusionToggle;

        public string RoomName { get { return roomNameInput.text; } }
        public int MaxPlayer { get
            {
                switch (maxPlayerDropdown.value)
                {
                    case 0:
                        return 2;
                    case 1:
                        return 3;
                    case 2:
                        return 4;
                    case 3:
                        return 5;
                    case 4:
                        return 6;
                    case 5:
                        return 7;
                    case 6:
                        return 8;
                    default:
                        return 8;
                }
            } 
        }
        public string GameType { get
            {
                return gameTypeDropdown.options[gameTypeDropdown.value].ToString();
            } }
        public float GameTime { get
            {
                switch (gameTimeDropdown.value)
                {
                    case 0:
                        return 5;
                    case 1:
                        return 10;
                    case 2:
                        return 15;
                    case 3:
                        return 20;
                    default: 
                        return 15;
                }
            } }
        public int MaxKill { get
            {
                switch (maxKillDropdown.value)
                {
                    case 0:
                        return 10;
                    case 1:
                        return 15;
                    case 2:
                        return 20;
                    case 3:
                        return 25;
                    case 4:
                        return 30;
                    default: 
                        return 20;
                }
            } }
        public bool Intrusion { get
            {
                return intrusionToggle.isOn;
            } }

        protected override void Awake()
        {
            base.Awake();

            buttons["ReturnButton"].onClick.AddListener(OnReturnButtonClicked);
            buttons["MakeRoomButton"].onClick.AddListener(CreateRoomConfirm);
        }

        private void OnEnable()
        {
            roomNameInput.text = $"room {Random.Range(1000, 2000)}";
        }

        public void CreateRoomConfirm()
        {
            RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = MaxPlayer };
            PhotonHashtable RoomCustomProps = new PhotonHashtable()
            {
                {"IsPlaying" , false},
                { "GameType", GameType},                
                {"GameTime", GameTime},
                {"MaxKill", MaxKill },
                {"Intrusion", Intrusion}
            };

            roomOptions.CustomRoomProperties = RoomCustomProps;
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "IsPlaying", "GameType" };

            //방만들기 시도
            if (PhotonNetwork.CreateRoom(RoomName, roomOptions))
            {
                this.gameObject.SetActive(false);
                GameManager.UI.CreatePopUpMessage("Make Room Success");
            }
            else
            {
                GameManager.UI.CreatePopUpMessage("Make Room Failed");
            }

        }

        private void OnReturnButtonClicked()
        {
            this.gameObject.SetActive(false);
        }
    }
}

