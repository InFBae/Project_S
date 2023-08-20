using JBB;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabUI : BaseUI
{
    [SerializeField] RankingBoardUI rankingBoardUI;
    protected override void Awake()
    {
        base.Awake();

    }

    private void OnEnable()
    {
        if (PhotonNetwork.InRoom)
        {
            texts["RoomText"].text = $"{PhotonNetwork.CurrentRoom.Name}";
        }
        rankingBoardUI.UpdateRankerList();

        for (int i = 0; i < 8; i++)
        {
            if (rankingBoardUI.rankers[i] != null)
            {
                texts[$"player{i + 1}"].text = $"{rankingBoardUI.rankers[i].GetNickname()}";
                texts[$"Kill_{i + 1}"].text = $"{rankingBoardUI.rankers[i].GetKillCount()}";
                texts[$"Death_{i + 1}"].text = $"{rankingBoardUI.rankers[i].GetDeathCount()}";
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
    
