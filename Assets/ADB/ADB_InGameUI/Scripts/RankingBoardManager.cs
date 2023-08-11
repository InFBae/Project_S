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
        // 플레이어 넘버링에 따라 리스트 추가
        //int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;    // 현재 방에 있는 플레이어 수
        int playerCount = PhotonNetwork.PlayerList.Length;    // 현재 방에 있는 플레이어 수

        rankingArray = new PlayerRankingInfo[playerCount];          // 플레이어 수만큼 배열 만들기

        int i = 0;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            rankingArray[i++].nickName = player.NickName;      // 플레이어의 닉네임을 가져오고
        }
    }
}
