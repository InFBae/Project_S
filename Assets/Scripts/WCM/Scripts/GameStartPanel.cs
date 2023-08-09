using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MySql.Data.MySqlClient;

public class GameStartPanel : MonoBehaviour
{
    //���� �Է��� �г� (�α��ΰ�)
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

            // ���� Ȯ��
            Debug.Log("DataBase connect success");
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    //���������� ������ؾ��ϹǷ� �����ϰ� ������ ����.
    /*private void OnEnable()
    {
        idInputField.text = string.Format("player {0}", Random.Range(1000, 10000));
    }*/

    //�α��� �Լ� -> ��ư����
    public void Login()
    {
        try
        {
            string id = idInputField.text;
            string pass = passwordInputField.text;

            string sqlCommand = string.Format("SELECT U_ID,U_Password FROM user_info WHERE U_ID='{0}'", id);
            MySqlCommand cmd = new MySqlCommand(sqlCommand, con);
            reader = cmd.ExecuteReader();

            // ������ �о��µ� ������� ������� ����
            if (reader.HasRows)
            {
                //�������� ������ ���پ� �а���
                while (reader.Read())
                {
                    string readId = reader["U_ID"].ToString();
                    string readPass = reader["U_Password"].ToString();

                    Debug.Log($"ID : {readId}, password : {readPass}");

                    if (pass == readPass)
                    {
                        //�̸��� ���� ��� �ش� �÷��̾��� �̸��� inputField���� ������ ���� (�̸� �ߺ����� �ʰ�)
                        PhotonNetwork.LocalPlayer.NickName = idInputField.text;

                        //���� ��Ʈ��ũ ������ ���� �õ�
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
