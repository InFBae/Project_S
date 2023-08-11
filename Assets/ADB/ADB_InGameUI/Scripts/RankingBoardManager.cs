using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingBoardManager : MonoBehaviourPunCallbacks
{
    RankingBoardUI rankingUI;
    List<PlayerRankingInfo> rankingList = new List<PlayerRankingInfo>();
    PlayerRankingInfo[] rankingArray;

    private void Start()
    {
        // �÷��̾� �ѹ����� ���� ����Ʈ �߰�
        //int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;    // ���� �濡 �ִ� �÷��̾� ��
        int playerCount = PhotonNetwork.PlayerList.Length;    // ���� �濡 �ִ� �÷��̾� ��

        rankingArray = new PlayerRankingInfo[playerCount];          // �÷��̾� ����ŭ �迭 �����

        int i = 0;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            rankingArray[i++].nickName = player.NickName;      // �÷��̾��� �г����� ��������
        }
    }
}
