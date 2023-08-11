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
    public class RoomSettingUI : BaseUI
    {
        [SerializeField] TMP_InputField roomNameInput;
        [SerializeField] TMP_Dropdown maxPlayerDropdown;
        [SerializeField] TMP_Dropdown gameTypeDropdown;
        [SerializeField] TMP_Dropdown gameTimeDropdown;
        [SerializeField] TMP_Dropdown maxKillDropdown;
        [SerializeField] Toggle intrusionToggle;

        public string RoomName { get { return roomNameInput.text; } }
        public int MaxPlayer
        {
            get
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
        public string GameType
        {
            get
            {
                switch (gameTypeDropdown.value)
                {
                    case 0:
                        return "SOLO";
                    default:
                        return "SOLO";
                }
            }
        }
        public float GameTime
        {
            get
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
            }
        }
        public int MaxKill
        {
            get
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
            }
        }
        public bool Intrusion
        {
            get
            {
                return intrusionToggle.isOn;
            }
        }



        protected override void Awake()
        {
            base.Awake();

            buttons["ReturnButton"].onClick.AddListener(OnReturnButtonClicked);
            buttons["ApplyButton"].onClick.AddListener(OnApplyButtonClicked);
        }

        private void OnEnable()
        {
            roomNameInput.text = PhotonNetwork.CurrentRoom.Name;
            switch (PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                case 2:
                    maxPlayerDropdown.value = 0;
                    break;
                case 3:
                    maxPlayerDropdown.value = 1;
                    break;
                case 4:
                    maxPlayerDropdown.value = 2;
                    break;
                case 5:
                    maxPlayerDropdown.value = 3;
                    break;                       
                case 6:
                    maxPlayerDropdown.value = 4;
                    break;
                case 7:
                    maxPlayerDropdown.value = 5;
                    break;
                case 8:
                    maxPlayerDropdown.value = 6;
                    break;
                default:
                    maxPlayerDropdown.value = 6;
                    break;
            }
            if (PhotonNetwork.CurrentRoom.GetGameType() == "SOLO")
            {
                gameTypeDropdown.value = 0;
            }
            else
            {
                gameTypeDropdown.value = 0;
            }

            if (PhotonNetwork.CurrentRoom.GetGameTime() == 5)
            {
                gameTimeDropdown.value = 0;
            }
            else if(PhotonNetwork.CurrentRoom.GetGameTime() == 10)
            {
                gameTimeDropdown.value = 1;
            }
            else if (PhotonNetwork.CurrentRoom.GetGameTime() == 15)
            {
                gameTimeDropdown.value = 2;
            }
            else if (PhotonNetwork.CurrentRoom.GetGameTime() == 20)
            {
                gameTimeDropdown.value = 3;
            }
            else
            {
                gameTypeDropdown.value = 2;
            }

            switch (PhotonNetwork.CurrentRoom.GetMaxKill())
            {
                case 10:
                    maxKillDropdown.value = 0; 
                    break;
                case 15:
                    maxKillDropdown.value = 1;
                    break;
                case 20:
                    maxKillDropdown.value = 2;
                    break;
                case 25:
                    maxKillDropdown.value = 3;
                    break;
                case 30:
                    maxKillDropdown.value = 4;
                    break;
                default:
                    maxKillDropdown.value = 2;
                    break;
            }

            if (PhotonNetwork.CurrentRoom.GetIntrusion())
            {
                intrusionToggle.isOn = true;
            }
            else
            {
                intrusionToggle.isOn = false;
            }
        }

        public void OnReturnButtonClicked()
        {
            this.gameObject.SetActive(false);
        }

        public void OnApplyButtonClicked()
        {
            PhotonHashtable RoomCustomProps = new PhotonHashtable()
            {
                {"IsPlaying" , false},
                { "GameType", GameType},
                {"GameTime", GameTime},
                {"MaxKill", MaxKill },
                {"Intrusion", Intrusion}
            };

            PhotonNetwork.CurrentRoom.MaxPlayers = MaxPlayer;
            PhotonNetwork.CurrentRoom.SetCustomProperties(RoomCustomProps);
            
            this.gameObject.SetActive(false);
        }
    }
}

