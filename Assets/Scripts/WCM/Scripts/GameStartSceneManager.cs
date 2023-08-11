using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using MySql.Data.MySqlClient;
using System;
using TMPro;
using UnityEngine.Animations.Rigging;
//using UnityEditor.MemoryProfiler;
//using UnityEditor.Search;
//using UnityEditor.Rendering.Universal;

public class GameStartSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject gameStartUI;
    [SerializeField] GameObject loginUI;
    [SerializeField] GameObject signInUI;


    [SerializeField] TMP_InputField idInputField;
    [SerializeField] TMP_InputField passwordInputField;
    [SerializeField] TMP_InputField idInputField_;
    [SerializeField] TMP_InputField passwordInputField_;
    [SerializeField] TMP_InputField passwordInputField_check;

    private MySqlConnection con;
    private MySqlDataReader reader;

    public void Start()
    {
        ConnectDataBase();
        
        /*if (PhotonNetwork.IsConnected)
            OnConnectedToMaster();
        else if (PhotonNetwork.InRoom)
            OnJoinedRoom();
        else if (PhotonNetwork.InLobby)
            OnJoinedLobby();
        else
            OnDisconnected(DisconnectCause.None);*/
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

    public void OpenLoginUI()
    {
        loginUI.SetActive(true);
    }
    public void CloseLoginUI()
    {
        loginUI?.SetActive(false);  
    }
    public void OpenSignInUI()
    {
        signInUI.SetActive(true);
    }
    public void CloseSignInUI()
    {
        signInUI?.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //로그인 함수 -> 버튼연동
    public void Login()
    {
        try
        {
            string id = idInputField.text;
            string pass = passwordInputField.text;

            string sqlCommand = string.Format("SELECT U_ID,U_Password,U_Nickname FROM user_info WHERE U_ID='{0}'", id);
            MySqlCommand cmd = new MySqlCommand(sqlCommand, con);
            reader = cmd.ExecuteReader();

            // 리더가 읽었는데 있을경우 없을경우 구분
            if (reader.HasRows)
            {
                //여러줄이 있으면 한줄씩 읽개됨
                while (reader.Read())
                {
                    string readId = reader["U_ID"].ToString();
                    string readPass = reader["U_Password"].ToString();
                    string readNick = reader["U_Nickname"].ToString();

                    Debug.Log($"ID : {readId}, password : {readPass}");

                    if (pass == readPass)
                    {
                        if (!reader.IsClosed)
                            reader.Close();

                        con.Close();
                        //이름이 있을 경우 해당 플레이어의 이름을 inputField내의 값으로 지정 (이름 중복되지 않게)
                        PhotonNetwork.LocalPlayer.NickName = readId;


                        PhotonNetwork.ConnectUsingSettings();

                        PhotonNetwork.LoadLevel("LobbyScene_");
                        //GameManager.Scene.LoadScene("LobbyScene_");

                        //이후 네트워크 서버에 연결 시도
                        //PhotonNetwork.ConnectUsingSettings();
                    }
                    else
                    {
                        Debug.Log("Wrong password");
                        if (!reader.IsClosed)
                            reader.Close();
                        return;
                    }
                }
            }
            else
            {
                if (!reader.IsClosed)
                    reader.Close();
                Debug.Log("There is no player id");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            if (!reader.IsClosed)
                reader.Close();
        }
    }

    public void SignIn()
    {
        try
        {
            string id = idInputField_.text;
            string password = passwordInputField_.text;
            string passwordCheck = passwordInputField_check.text;

            if (password != passwordCheck)
            {
                Debug.Log("password don't match");
                return;
            }

            else
            {
                string sqlCommand = string.Format("SELECT U_ID FROM user_info WHERE U_ID='{0}'", id);
                MySqlCommand cmd = new MySqlCommand(sqlCommand, con);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Debug.Log("same name exist");
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }

                }

                else
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    string sqlCommand2 = $"INSERT INTO user_info(U_ID, U_Password) VALUES({id}, {password})";
                    
                    GameManager.DB.ExecuteNonQuery(sqlCommand2);
                    Debug.Log("Complete sign up");
                    CloseSignInUI();
                }
                /*else
                {
                    string sqlCommand2 = string.Format("INSERT INTO user_info (U_ID, U_Password) VALUES ('{0}','{1}')", id, password);
                    using (MySqlCommand command = new MySqlCommand(sqlCommand2, con))
                    {
                        command.Parameters.AddWithValue("U_ID", id);
                        command.Parameters.AddWithValue("U_Password",password);
                    }
                        new MySqlCommand(sqlCommand2, con);
                }
                if (!reader.IsClosed)
                    reader.Close();*/
            }
        }

        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    //지속적으로 디버깅해야하므로 랜덤하게 들어가도록 설정.
    /*private void OnEnable()
    {
        idInputField.text = string.Format("Player {0}", Random.Range(1000, 10000));
    }*/

    /*
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            SetActivePanel(Panel.Menu);
            Debug.Log($"Create room failed with error({returnCode}) : {message}");
            statePanel.AddMessage($"Create room failed with error({returnCode}) : {message}");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            SetActivePanel(Panel.Menu);
            Debug.Log($"Join room failed with error({returnCode}) : {message}");
            statePanel.AddMessage($"Join room failed with error({returnCode}) : {message}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            SetActivePanel(Panel.Menu);
            Debug.Log($"Join random room failed with error({returnCode}) : {message}");
            statePanel.AddMessage($"Join random room failed with error({returnCode}) : {message}");
        }

        public override void OnJoinedRoom()
        {
            SetActivePanel(Panel.Room);
        }

        public override void OnLeftRoom()
        {
            SetActivePanel(Panel.Menu);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            roomPanel.PlayerEnterRoom(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            roomPanel.PlayerLeftRoom(otherPlayer);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, PhotonHashtable changedProps)
        {
            roomPanel.PlayerPropertiesUpdate(targetPlayer, changedProps);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            roomPanel.MasterClientSwitched(newMasterClient);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            lobbyPanel.UpdateRoomList(roomList);
        }*/

    /*public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("connecting");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined lobby");
    }*/
}
