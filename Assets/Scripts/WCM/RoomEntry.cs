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
        //방의 찬 인원수 보여줌
        currentPlayer.text = $"{roomInfo.PlayerCount} / {roomInfo.MaxPlayers}";

        //수정예정사항
        //rockOption.gameObject.SetActive(!PhotonNetwork.CurrentRoom.GetRoomInfo_canIrrupt());

        //수정예정사항
        /*if (PhotonNetwork.CurrentRoom.GetRoomInfo_IsPlayingNow())
        {
            isPlaying.text = "playing";
        }
        else { isPlaying.text = "nonPlaying"; };*/
    }

    // join버튼과 연동되어 방에 들어가는것을 진행
    // 포톤의 특징이긴한데 로비를 나가고 들어가야함 -> 포톤의경우
    // 로비에 그대로 존재하는것으로 인식하므로 갱신작업을 반복하기때문에 부하가생길수있음
    public void JoinRoom()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.JoinRoom(info.Name);
    }
}
