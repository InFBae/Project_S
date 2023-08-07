using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private MySqlConnection con;
    private void Awake()
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
    public MySqlDataReader Execute(string sqlCommand)
    {
        MySqlCommand cmd = new MySqlCommand(sqlCommand, con);
        return cmd.ExecuteReader();
    }

    public void ExecuteNonQuery(string sqlCommand)
    {
        MySqlCommand cmd = new MySqlCommand(sqlCommand, con);
        cmd.ExecuteNonQuery();
    }
}
