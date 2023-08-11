using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomEntry : MonoBehaviour
{
    [SerializeField] TMP_Text roomName;
    [SerializeField] TMP_Text currentPlayer;
    [SerializeField] RectTransform rockOption;
    [SerializeField] TMP_Text isPlaying;

    public RoomInfo info;

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        info = roomInfo;

        roomName.text = roomInfo.Name;
        //���� �� �ο��� ������
        currentPlayer.text = $"{roomInfo.PlayerCount} / {roomInfo.MaxPlayers}";

        //������������
        //rockOption.gameObject.SetActive(!PhotonNetwork.CurrentRoom.GetRoomInfo_canIrrupt());

        //������������
        /*if (PhotonNetwork.CurrentRoom.GetRoomInfo_IsPlayingNow())
        {
            isPlaying.text = "playing";
        }
        else { isPlaying.text = "nonPlaying"; };*/
    }

    // join��ư�� �����Ǿ� �濡 ���°��� ����
    // ������ Ư¡�̱��ѵ� �κ� ������ ������ -> �����ǰ��
    // �κ� �״�� �����ϴ°����� �ν��ϹǷ� �����۾��� �ݺ��ϱ⶧���� ���ϰ����������
    public void JoinRoom()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.JoinRoom(info.Name);
    }
}
