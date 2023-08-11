using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JBB
{
    public class PlayerListUI : BaseUI
    {
        PlayerSlotUI[] playerSlots;
        protected override void Awake()
        {
            base.Awake();

            playerSlots = GetComponentsInChildren<PlayerSlotUI>();
            foreach (PlayerSlotUI slot in playerSlots)
            {
                slot.gameObject.SetActive(false);
            }
        }

        public void UpdatePlayerList()
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerSlots[i].gameObject.SetActive(true);
                playerSlots[i].SetPlayerSlot(PhotonNetwork.PlayerList[i]);
            }
            for (int i = PhotonNetwork.PlayerList.Length; i < 8; i++)
            {
                playerSlots[i].gameObject.SetActive(false);
            }
        }

        public void UpdatePlayerSlot(Player player)
        {
            for (int i = 0; i < 8; i++)
            {
                if (playerSlots[i].GetPlayer() == player)
                {
                    playerSlots[i].SetPlayerSlot(player);
                }
            }
        }
    }
}

