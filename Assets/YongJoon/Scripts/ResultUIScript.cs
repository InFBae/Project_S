using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JBB;
using Photon.Pun;
using Photon.Realtime;

public class ResultUIScript : BaseUI
{
    [SerializeField] RankingBoardUI rankingBoardUI;

    protected override void Awake()
    {
        base.Awake();
        buttons["ReturnButton"].onClick.AddListener(OnReturnToRoomButtonClicked);
    }

    public void OnEnable()
    {
        for (int i = 0; i < 8; i++)
        {
            if (rankingBoardUI.rankers[i] != null)
            {
                texts[$"Rank{i + 1}"].text = $"{i + 1}";
                texts[$"Nickname{i + 1}"].text = $"{rankingBoardUI.rankers[i].GetNickname()}";
                texts[$"KillDeath{i + 1}"].text = $"{rankingBoardUI.rankers[i].GetKillCount()}" + "/" + $"{rankingBoardUI.rankers[i].GetDeathCount()}";
                texts[$"Exp{i + 1}"].text = $"{1000 / (i +1) }";
                texts[$"Ect{i + 1}"].text = "";
            }
            else
            {
                texts[$"Rank{i + 1}"].text = "";
                texts[$"Nickname{i + 1}"].text = "";
                texts[$"KillDeath{i + 1}"].text = "";
                texts[$"Exp{i + 1}"].text = "";
                texts[$"Ect{i + 1}"].text = "";
            }
        }
    }
    private void OnReturnToRoomButtonClicked()
    {
        PhotonNetwork.CurrentRoom.SetIsPlaying(false);
        PhotonNetwork.CurrentRoom.SetIsEnd(true);
        PhotonNetwork.CurrentRoom.IsOpen = true;
        GameManager.Scene.LoadScene("RoomScene");

    }
}
