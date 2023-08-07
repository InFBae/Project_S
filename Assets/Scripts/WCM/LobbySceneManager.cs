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
using UnityEngine.Animations.Rigging;
using static UnityEngine.Rendering.DebugUI;

public class LobbySceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField nickNameInputField;
    [SerializeField] GameObject nickNamePopUp;

    private MySqlDataReader reader;
    private MySqlConnection con;
    // Start is called before the first frame update
    void Start()
    {
        ConnectDataBase();

        if (NickNameChecking())
        {
            OpenNicknameSetting();
        }
        GameManager.Chat.Connect(PhotonNetwork.LocalPlayer.NickName);
    }

    private void ConnectDataBase()
    {
        try
        {
            string serverInfo = "Server=43.200.178.18; Database=userdb; Uid=root; Pwd=1234; Port=3306; CharSet=utf8;";
            con = new MySqlConnection(serverInfo);
            con.Open();

            // ���� Ȯ��
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
        //nickname �Լ�����
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

        if(nick == "")
        {
            Debug.Log("please make your username");
            return;
        }
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
            GameManager.Chat.Connect(PhotonNetwork.LocalPlayer.NickName);
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
        PhotonNetwork.LeaveLobby();
    }

    //�游��� ���������� ?
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // �游��Ⱑ �����Ұ�� menuȭ�� ���ư�����


        // � ������ �����ߴ��� �α� �����
        Debug.Log($"create room failed with error({returnCode}) : {message}");
        //statePanel.AddMessage($"create room failed with error({returnCode}) : {message}");
    }
    //�ѤѤѤѤѤѤѤѤѤѤѤѤѤ�
}
