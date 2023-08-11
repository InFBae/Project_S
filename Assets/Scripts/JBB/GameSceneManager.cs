using Photon.Pun;
using Photon.Realtime;
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
            
        }

        public override void OnEnable()
        {
            base.OnEnable();

            OnKilled.AddListener(ChangeKillDeathProperty);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            OnKilled.RemoveListener(ChangeKillDeathProperty);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, PhotonHashtable changedProps)
        {
            if (targetPlayer == PhotonNetwork.LocalPlayer)
            {
                inGameUI.UpdateKillDeathUI();
            }
        }
        public override void OnRoomPropertiesUpdate(PhotonHashtable propertiesThatChanged)
        {
            if (propertiesThatChanged.ContainsKey("AllLoaded"))
            {

            }
        }

        public void ChangeKillDeathProperty(Player killed, Player dead, bool isHeadShot)
        {
            killed.SetKillCount(killed.GetKillCount() + 1);
            dead.SetDeathCount(dead.GetDeathCount() + 1);
        }

    }
}

