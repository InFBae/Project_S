using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class RankingBoardManager : MonoBehaviourPunCallbacks
{
    RankingBoardUI rankingUI;
    [SerializeField] PlayerRankingInfo[] rankingArray;
    [SerializeField] GameObject playerRankingInfoUI;
    [SerializeField] GameObject playerRankingBoard;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    private void Start()
    {
        RankingBoardSetting();
    }

    public void RankingBoardSetting()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;    // ���� �濡 �ִ� �÷��̾� ��
        rankingArray = new PlayerRankingInfo[playerCount];          // �÷��̾� ����ŭ �迭 �����

        int i = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            rankingArray[i++].killCount = ADB_CustomProperty.GetKillCount(player);
            playerRankingBoard = Instantiate<GameObject>(playerRankingInfoUI, playerRankingInfoUI.transform.position, Quaternion.identity);
            playerRankingBoard.transform.parent = playerRankingInfoUI.transform;
            // Playerinfo UI �����ؼ� RankingBoard �ڽ����� ����
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("KillCount"))
        {
        }
    }
}
