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

    //�� ������� �ʱ�ȭ
    public void UpdatePlayerList()
    {
        for (int i= 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // ȣ��Ʈ�� ����ǥ��
                playerSlots.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }

            // ����μ���ŭ ������Ʈ Ȱ��ȭ
            playerSlots.GetChild(i).gameObject.SetActive(true);

            // nickname ����
            playerSlots.GetChild(i).GetChild(0);
            
        }
    }


    //���ӽ���
    public void StartGame()
    {
        PhotonNetwork.LoadLevel("GameScnee");
    }

    /*//�泪���� �Լ�
    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        //ê Ŭ���̾�Ʈ chatclient.Subscribe["noticechannel"];
    }*/
}
