using MySql.Data.MySqlClient;
using Photon.Realtime;
using Photon.Pun;
using System;
using UnityEngine;
using TMPro;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;
using System.Collections.Generic;


public class LobbySceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField nickNameInputField;
    [SerializeField] GameObject nickNamePopUp;
    [SerializeField] GameObject lobbyUI;
    [SerializeField] GameObject roomUI;

    [SerializeField] RoomEntry roomEntry;
    [SerializeField] RectTransform roomContent;

    Dictionary<string, RoomInfo> roomDic;

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

    //룸 dic생성
    private void Awake()
    {
        roomDic = new Dictionary<string, RoomInfo>();
    }
    
    //룸 list 업데이트
    public void UpdateRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomContent.childCount; i++)
        {
            Destroy(roomContent.GetChild(i).gameObject);
        }

        // 룸 리스트 update
        foreach (RoomInfo info in roomList)
        {
            // 방이 사라졌으면
            // photon 에서 제공해주는 roominfo 안에 사라질 예정인 방을 마킹해놓음 + 방이 비공개가 되었을 경우 + 방이 닫혔으면(게임시작)
            if (info.RemovedFromList || !info.IsVisible || !info.IsOpen)
            {
                // 삭제예정인 방이지만 dic에 추가가 안된경우도 존재 할 수 있음.
                if (roomDic.ContainsKey(info.Name))
                {
                    roomDic.Remove(info.Name);
                }
                continue;
            }

            // 방이 변경됐으면
            // 이름이 있었던 방일경우 최신으로 갱신 dic내에 존재하는 방이면
            if (roomDic.ContainsKey(info.Name))
            {
                roomDic[info.Name] = info;
            }

            // 방이 생성됐으면
            else
            {
                roomDic.Add(info.Name, info);
            }
        }

        // create room list
        // Dic이 초기화되었으면 해당 자료형을 사용해서 새로 방구성
        foreach (RoomInfo info in roomDic.Values)
        {
            RoomEntry entry = Instantiate(roomEntry, roomContent.transform);
            entry.SetRoomInfo(info);
        }
    }
    public void JoinRoom()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.JoinRoom(PhotonNetwork.CurrentRoom.Name);
    }

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

            // 성공 확인
            Debug.Log("DataBase connect success");
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    public void CreateRoomConfirm()
    {
        //룸 만들었을때 ?
        // 로비에서 나가서 룸으로 들어가고
        // 로비챗 구독 취소 해주고
        string roomName = $"room {UnityEngine.Random.Range(1000,2000)}";
        

        //값 받아오기
        int maxPlayer = maxPlayerInput.text == "" ? 8 : int.Parse(maxPlayerInput.text);
        string gameTime = gameTimeInput.text;
        int maxKill = int.Parse(maxPlayerInput.text);
        bool canIrrupt = roomMenu.GetComponentInChildren<Toggle>().isOn;

        // 방설정 해주고 
        //RoomOptions options = new RoomOptions { MaxPlayers = (byte)maxPlayer };

        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = maxPlayer };
        PhotonHashtable RoomCustomProps = new PhotonHashtable()
        {
            {"IsPlayingNow" , false},
            {"gameTime", gameTime},
            {"maxKill", maxKill },
            {"canIrrupt", canIrrupt}
        };

        //PhotonHashtable PlayerCustomProps = new PhotonHashtable() { { "Nickname", nick} };

        //Player player = null;
        //player.CustomProperties = PlayerCustomProps;


        roomOptions.CustomRoomProperties = RoomCustomProps;
       
        //방만들기 시도
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        
        //Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["gameTime"].ToString());
    }

    public override void OnRoomListUpdate(List<Photon.Realtime.RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
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
            PhotonNetwork.LocalPlayer.SetCustomProperties(new PhotonHashtable { { "Nick", nick } });

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
                PhotonNetwork.LocalPlayer.SetCustomProperties(new PhotonHashtable { { "Nick", readNick } });
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
        roomMenu.SetActive(false);
        lobbyUI.SetActive(false);
        roomUI.SetActive(true);

        string Nick = PhotonNetwork.LocalPlayer.GetNick();
        PhotonNetwork.LocalPlayer.SetReady(false);
        PhotonNetwork.LocalPlayer.SetLoad(false);

        Debug.Log(Nick);
        //자동 동기화
        //PhotonNetwork.AutomaticallySyncScene = true;
        UpdateRoomSetting();
    }

    public void UpdateRoomSetting()
    {
        a = PhotonNetwork.CurrentRoom.GetRoomInfo_gameTime();
        b = PhotonNetwork.CurrentRoom.GetRoomInfo_canIrrupt();
        c = PhotonNetwork.CurrentRoom.GetRoomInfo_maxKill();
    }
    public override void OnRoomPropertiesUpdate(PhotonHashtable propertiesThatChanged)
    {
        UpdateRoomSetting();
    }

    //룸 인포 값 변경
    public void RoomInfoChange()
    {
        //현재 room properties값 읽어와서 roominfo change ui열기
        roomSetting.SetActive(true);
    }

    public void RoomInfoChangeConfirm()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        //roominfo 가 바뀐값 참고해서 룸 프로퍼티 변경
        roomSetting.SetActive(false);
    }

    //방만들기 실패했을때 ?
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // 어떤 이유로 실패했는지 로그 찍어줌
        Debug.Log($"create room failed with error({returnCode}) : {message}");
        //statePanel.AddMessage($"create room failed with error({returnCode}) : {message}");
    }
    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        roomUI.SetActive(false);
        lobbyUI.SetActive(true);
        //챗 클라이언트 chatclient.Subscribe["noticechannel"];
    }
}