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
                texts[$"nick{i}"].text = $"{rankingBoardUI.rankers[i].GetNickname()}";
                texts[$"kill{i}"].text = $"{rankingBoardUI.rankers[i].GetKillCount()}";
                texts[$"death{i}"].text = $"{rankingBoardUI.rankers[i].GetDeathCount()}";
            }
            else
            {
                texts[$"nick{i}"].text = "";
                texts[$"kill{i}"].text = "";
                texts[$"death{i}"].text = "";
            }
        }
    }

}
    