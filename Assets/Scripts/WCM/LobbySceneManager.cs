using MySql.Data.MySqlClient;
using Photon.Pun;
using System;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using ExitGames.Client.Photon;

public class LobbySceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField nickNameInputField;
    [SerializeField] GameObject nickNamePopUp;
    [SerializeField] GameObject lobbyUI;
    [SerializeField] GameObject roomUI;


    private MySqlDataReader reader;
    private MySqlConnection con;
    // Start is called before the first frame update

    [SerializeField] GameObject roomMenu;
    [SerializeField] GameObject roomSetting;
    [SerializeField] TMP_Text maxPlayerInput;
    [SerializeField] TMP_Text gameTimeInput;
    [SerializeField] TMP_Text maxKillInput;

    [SerializeField] string a;
    [SerializeField] bool b;
    [SerializeField] float c;



    void Start()
    {
        ConnectDataBase();

        /*if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = $"DebugPlayer{UnityEngine.Random.Range(1000, 10000)}";
            PhotonNetwork.ConnectUsingSettings();
        }*/
        
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

    public void CreateRoomConfirm()
    {
        //�� ��������� ?
        // �κ񿡼� ������ ������ ����
        // �κ�ê ���� ��� ���ְ�
        string roomName = $"room {UnityEngine.Random.Range(1000,2000)}";
        

        //�� �޾ƿ���
        int maxPlayer = maxPlayerInput.text == "" ? 8 : int.Parse(maxPlayerInput.text);
        string gameTime = gameTimeInput.text;
        int maxKill = int.Parse(maxPlayerInput.text);
        bool canIrrupt = roomMenu.GetComponentInChildren<Toggle>().isOn;

        // �漳�� ���ְ� 
        //RoomOptions options = new RoomOptions { MaxPlayers = (byte)maxPlayer };

        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = maxPlayer };
        Hashtable RoomCustomProps = new Hashtable()
        {
            {"gameTime", gameTime},
            {"maxKill", maxKill },
            {"canIrrupt", canIrrupt}
        };

        //Hashtable PlayerCustomProps = new Hashtable() { { "Nickname", nick} };

        Player player = null;
        //player.CustomProperties = PlayerCustomProps;


        roomOptions.CustomRoomProperties = RoomCustomProps;
       
        //�游��� �õ�
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        //bool dfaf = PhotonNetwork.CurrentRoom.GetRoomInfo_canIrrupt();
        //Debug.Log(dfaf);
        //Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["gameTime"].ToString());
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
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
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Nick", nick } });

            cmd2.ExecuteNonQuery();
            GameManager.Chat.Connect(PhotonNetwork.LocalPlayer.NickName);
            CloseNicknameSetting();
        }
    }

    public bool NickNameChecking()
    {
        string readNick;
        string id = PhotonNetwork.LocalPlayer.NickName;
        string sqlCommand = $"SELECT U_Nickname FROM user_info WHERE U_ID='{id}'";
        MySqlCommand cmd = new MySqlCommand(sqlCommand, con);
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            readNick = reader["U_Nickname"].ToString();
            if (readNick == "" || readNick == null)
            {
                if (!reader.IsClosed)
                    reader.Close();
                return true;
            }
            else
            {
                if (!reader.IsClosed)
                    reader.Close();
                PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Nick", readNick } });
                return false;
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

    public override void OnJoinedRoom()
    {
        lobbyUI.SetActive(false);
        roomUI.SetActive(true);
        roomMenu.SetActive(false);

        string Nick = PhotonNetwork.LocalPlayer.GetNick();
        PhotonNetwork.LocalPlayer.SetReady(false);
        PhotonNetwork.LocalPlayer.SetLoad(false);

        Debug.Log(Nick);
        //�ڵ� ����ȭ
        //PhotonNetwork.AutomaticallySyncScene = true;
        UpdateRoomSetting();
    }

    public void UpdateRoomSetting()
    {
        a = PhotonNetwork.CurrentRoom.GetRoomInfo_gameTime();
        b = PhotonNetwork.CurrentRoom.GetRoomInfo_canIrrupt();
        c = PhotonNetwork.CurrentRoom.GetRoomInfo_maxKill();
    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {

        UpdateRoomSetting();
    }

    //�� ���� �� ����
    public void RoomInfoChange()
    {
        //���� room properties�� �о�ͼ� roominfo change ui����
        roomSetting.SetActive(true);
    }

    public void RoomInfoChangeConfirm()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        //roominfo �� �ٲﰪ �����ؼ� �� ������Ƽ ����
        roomSetting.SetActive(false);
    }

    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    //�游��� ���������� ?
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // � ������ �����ߴ��� �α� �����
        Debug.Log($"create room failed with error({returnCode}) : {message}");
        //statePanel.AddMessage($"create room failed with error({returnCode}) : {message}");
    }
}