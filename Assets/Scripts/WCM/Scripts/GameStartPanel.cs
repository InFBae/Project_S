using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MySql.Data.MySqlClient;

public class GameStartPanel : MonoBehaviour
{
    //값을 입력할 패널 (로그인값)
    [SerializeField] TMP_InputField idInputField;
    [SerializeField] TMP_InputField passwordInputField;

    private MySqlConnection con;
    private MySqlDataReader reader;


    private void Start()
    {
        ConnectDataBase();
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

    //지속적으로 디버깅해야하므로 랜덤하게 들어가도록 설정.
    /*private void OnEnable()
    {
        idInputField.text = string.Format("player {0}", Random.Range(1000, 10000));
    }*/

    //로그인 함수 -> 버튼연동
    public void Login()
    {
        try
        {
            string id = idInputField.text;
            string pass = passwordInputField.text;

            string sqlCommand = string.Format("SELECT U_ID,U_Password FROM user_info WHERE U_ID='{0}'", id);
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

                    Debug.Log($"ID : {readId}, password : {readPass}");

                    if (pass == readPass)
                    {
                        //이름이 있을 경우 해당 플레이어의 이름을 inputField내의 값으로 지정 (이름 중복되지 않게)
                        PhotonNetwork.LocalPlayer.NickName = idInputField.text;

                        //이후 네트워크 서버에 연결 시도
                        PhotonNetwork.ConnectUsingSettings();
                        if (!reader.IsClosed)
                            reader.Close();
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
                Debug.Log("There is no player id");
            }
            if (!reader.IsClosed)
                reader.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
