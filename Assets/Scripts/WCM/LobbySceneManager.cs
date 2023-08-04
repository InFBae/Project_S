using MySql.Data.MySqlClient;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ExitGames.Client.Photon;
using UnityEngine.Rendering;
using Photon.Chat;

public class LobbySceneManager : MonoBehaviourPunCallbacks , IChatClientListener
{
    [SerializeField] TMP_InputField nickNameInputField;
    [SerializeField] GameObject nickNamePopUp;

    private MySqlDataReader reader;
    private MySqlConnection con;
    // Start is called before the first frame update
    void Start()
    {
        ConnectDataBase();

        PhotonNetwork.ConnectUsingSettings();

        if (NickNameChecking())
        {
            OpenNicknameSetting();
        }
    }

    private void ConnectDataBase()
    {
        try
        {
            string serverInfo = "Server=43.200.178.18; Database=userdb; Uid=root; Pwd=1234; Port=3306; CharSet=utf8;";
            con = new MySqlConnection(serverInfo);
            con.Open();

            // 성공 확인
            Debug.Log("DataBase connect success");
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(123);
    }


    public void OpenNicknameSetting()
    {
        nickNamePopUp.SetActive(true);
        //nickname 함수실행
    }
    public void CloseNicknameSetting()
    {
        nickNamePopUp.SetActive(false);
    }
    public void Confirm()
    {
        string id = PhotonNetwork.LocalPlayer.NickName;
        string nick = nickNameInputField.text;
        string query = $"SELECT U_ID FROM user_info WHERE U_Nickname = '{nick}'";
        MySqlCommand cmd = new MySqlCommand(query, con);
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Debug.Log("Same NickName is exist");
            if (!reader.IsClosed)
                reader.Close();
        }
        else
        {
            if (!reader.IsClosed)
                reader.Close();

            string query2 = $"UPDATE user_info SET U_Nickname='{nick}' WHERE U_ID = '{id}'";
            MySqlCommand cmd2 = new MySqlCommand(query2, con);
            cmd2.ExecuteNonQuery();
            CloseNicknameSetting();
            
        }
    }

    public bool NickNameChecking()
    {
        string nick2 = PhotonNetwork.LocalPlayer.NickName;
        string sqlCommand = $"SELECT U_Nickname FROM user_info WHERE U_ID='{nick2}'";
        MySqlCommand cmd = new MySqlCommand(sqlCommand, con);
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            string readNick = reader["U_Nickname"].ToString();
            if (readNick == "")
            {
                if (!reader.IsClosed)
                    reader.Close();
                return true;
            }
        
        }
        if (!reader.IsClosed)
            reader.Close();
        return false;
        
    }

    public void LogOut()
    {
        GameManager.Scene.LoadScene("GameStartScene_");
    }

    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnected()
    {
        throw new NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        throw new NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        throw new NotImplementedException();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        throw new NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new NotImplementedException();
    }
}
