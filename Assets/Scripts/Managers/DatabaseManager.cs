using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private MySqlConnection con;
    private MySqlDataReader reader;
    private void Awake()
    {
        ConnectDataBase();
    }

    private void ConnectDataBase()
    {
        try
        {
            string serverInfo = "Server=15.164.97.3; Database=userdb; Uid=user; Pwd=@VR6; Port=3306; CharSet=utf8;";
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
        if (reader != null && !reader.IsClosed)
            reader.Close();
        MySqlCommand cmd = new MySqlCommand(sqlCommand, con);
        reader = cmd.ExecuteReader();
        return reader;
    }

    public void ExecuteNonQuery(string sqlCommand)
    {
        if (reader != null && !reader.IsClosed)
            reader.Close();
        MySqlCommand cmd = new MySqlCommand(sqlCommand, con);
        cmd.ExecuteNonQuery();
    }
}
