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

public class LobbySceneManager : MonoBehaviour
{
    [SerializeField] TMP_InputField nickNameInputField;

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

    public void OpenNicknameSetting()
    {
        Debug.Log("open");
        //nickname 함수실행
    }
    public void CloseNicknameSetting()
    {
        // 동일
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
        }
        else
        {
            string query2 = $"UPDATE user_info SET U_Nickname='{nick}' WHERE U_ID = {id}";
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
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogOut()
    {
        GameManager.Scene.LoadScene("GameStartScene_");
    }
}
