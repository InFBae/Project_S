using UnityEngine;
using Photon.Pun;
using MySql.Data.MySqlClient;
using System;


namespace JBB
{
    public class GameStartSceneManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] GameStartUI gameStartUI;

        public override void OnEnable()
        {
            base.OnEnable();
            LogInUI.OnLogInClicked.AddListener(Login);
            SignInUI.OnSignInClicked.AddListener(SignIn);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            LogInUI.OnLogInClicked.RemoveListener(Login);
            SignInUI.OnSignInClicked.RemoveListener(SignIn);
        }

        public override void OnConnectedToMaster()
        {
            GameManager.Scene.LoadScene("LobbyScene");
        }

        public void Login(string id, string pass)
        {
            try
            {
                string sqlCommand = string.Format("SELECT U_ID,U_Password,U_Nickname FROM user_info WHERE U_ID='{0}'", id);
                MySqlDataReader reader = null;                
                reader = GameManager.DB.Execute(sqlCommand);

                // ������ �о��µ� ������� ������� ����
                if (reader.HasRows)
                {
                    //�������� ������ ���پ� �а���
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

                            //�̸��� ���� ��� �ش� �÷��̾��� �̸��� inputField���� ������ ���� (�̸� �ߺ����� �ʰ�)
                            PhotonNetwork.LocalPlayer.NickName = readId;

                            PhotonNetwork.ConnectUsingSettings();
                            GameManager.UI.CreatePopUpMessage("Connecting Server");
                            //GameManager.Scene.LoadScene("LobbyScene");
                        }
                        else
                        {
                            GameManager.UI.CreatePopUpMessage("Wrong password");
                            Debug.Log("Wrong password");
                            if (!reader.IsClosed)
                                reader.Close();
                            return;
                        }
                    }
                }
                else
                {
                    GameManager.UI.CreatePopUpMessage("There is no player id");
                    Debug.Log("There is no player id");
                    if (!reader.IsClosed)
                        reader.Close();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public void SignIn(string id, string password, string passwordCheck)
        {
            try
            {
                if (password != passwordCheck)
                {
                    GameManager.UI.CreatePopUpMessage("password don't match");
                    Debug.Log("password don't match");
                    return;
                }

                else
                {
                    string sqlCommand = string.Format("SELECT U_ID FROM user_info WHERE U_ID='{0}'", id);
                    MySqlDataReader reader = null;
                    reader = GameManager.DB.Execute(sqlCommand);

                    if (reader.HasRows)
                    {
                        GameManager.UI.CreatePopUpMessage("same name exist");
                        Debug.Log("same name exist");
                    }
                    else
                    {
                        string sqlCommand2 = $"INSERT INTO user_info(U_ID, U_Password) VALUES({id}, {password})";                        
                        GameManager.DB.ExecuteNonQuery(sqlCommand2);
                        Debug.Log("Complete sign up");
                        gameStartUI.CloseSignInUI();
                    }
                }
            }

            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}
