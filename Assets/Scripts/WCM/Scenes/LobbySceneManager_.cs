using MySql.Data.MySqlClient;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JBB;

public class LobbySceneManager_ : MonoBehaviourPunCallbacks
{
    [SerializeField] LobbyUI lobbyUI;
    //[SerializeField] LobbyUI lobbyUI;

    public override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.SceneLoadInit();
    }

    public override void OnDisable()
    {
        base.OnDisable();

    }

    public void Start()
    {
        string nick = NickNameChecking();
        if (nick == null || nick == "")
        {
            // 닉네임 생성
            lobbyUI.EnableNicknameUI();
            nick = NickNameChecking();
        }
        GameManager.Chat.Connect(nick);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public string NickNameChecking()
    {
        string id = PhotonNetwork.LocalPlayer.NickName;
        string sqlCommand = $"SELECT U_Nickname FROM user_info WHERE U_ID='{id}'";
        MySqlDataReader reader = null;
        reader = GameManager.DB.Execute(sqlCommand);

        if (reader.HasRows)
        {
            reader.Read();
            string readNick = reader["U_Nickname"].ToString();
            return readNick;
        }
        return null;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        lobbyUI.UpdateRoomList(roomList);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LeaveLobby();
        GameManager.Chat.LeaveLobbyChannel();

        Debug.Log($"{GameManager.Chat.Nickname} Joined Room");
        PhotonNetwork.LocalPlayer.SetNickname(GameManager.Chat.Nickname);
        PhotonNetwork.LocalPlayer.SetReady(false);
        PhotonNetwork.LocalPlayer.SetLoad(false);
        GameManager.Scene.LoadScene("RoomScene_");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // 어떤 이유로 실패했는지 로그 찍어줌
        Debug.Log($"Join Room Failed With Error({returnCode}) : {message}");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Create Room Success");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // 어떤 이유로 실패했는지 로그 찍어줌
        Debug.Log($"create room failed with error({returnCode}) : {message}");
    }
}

