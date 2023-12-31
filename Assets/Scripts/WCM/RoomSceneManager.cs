using MySql.Data.MySqlClient;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] RectTransform playerSlots;
    [SerializeField] RectTransform room;

    public Player[] playerList;

    ChatClient chatclient;

    //방 유저목록 초기화
    public void UpdatePlayerList()
    {
        for (int i= 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // 호스트만 방장표시
                playerSlots.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }

            // 사람인수만큼 오브젝트 활성화
            playerSlots.GetChild(i).gameObject.SetActive(true);

            // nickname 변경
            playerSlots.GetChild(i).GetChild(0);
            
        }
    }


    //게임시작
    public void StartGame()
    {
        PhotonNetwork.LoadLevel("GameScnee");
    }

    /*//방나가는 함수
    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        //챗 클라이언트 chatclient.Subscribe["noticechannel"];
    }*/
}
