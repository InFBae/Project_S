using JBB;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Player = Photon.Realtime.Player;

public class UserInfoUI : BaseUI
{
    Player[] RankList = new Player[PhotonNetwork.CurrentRoom.PlayerCount];
    protected override void Awake()
    {
        base.Awake();   
    }

    
    public void UpdateList()
    {
        Player[] currentPlayers = PhotonNetwork.PlayerList;
        Array.Sort(currentPlayers, ((Player a, Player b) => { return a.GetKillCount() > b.GetKillCount() ? -1 : 1; }));

        for (int i = 0; i < RankList.Length; i++)
        {
            if (i < currentPlayers.Length)
            {
                RankList[i] = currentPlayers[i];
            }
        }
    }

    public void UpdateDashBoard()
    {
        for(int i=0; i< RankList.Length; i++)
        {
            if (RankList[i] != null)
            {
                texts[$"player{i + 1}"].text = RankList[i].GetNickname();
                texts[$"Kill_{i + 1}"].text = RankList[i].GetKillCount().ToString();
                texts[$"Death_{i + 1}"].text = RankList[i].GetDeathCount().ToString();
            }
            else
            {
                texts[$"player{i + 1}"].text = "";
                texts[$"Kill_{i + 1}"].text = "";
                texts[$"Death_{i + 1}"].text = "";
            }
        }
    }
}
