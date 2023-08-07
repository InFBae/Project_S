using ExitGames.Client.Photon;
using Photon.Chat;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using AuthenticationValues = Photon.Chat.AuthenticationValues;
using UnityEngine.Events;
using MySql.Data.MySqlClient;
using JBB;
using static UnityEditor.ShaderData;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    [SerializeField] private ChatClient chatClient;
    [SerializeField] private string nickname;
    private string lobbyChannel;
    private string noticeChannel;

    public static UnityEvent<string> OnGetLobbyMessage = new UnityEvent<string>();

    protected internal ChatAppSettings chatAppSettings;

    public ChatAppSettings ChatAppSettings
    {
        get { return this.chatAppSettings; }
    }

    private void Start()
    {
        Application.runInBackground = true;

        chatClient = new ChatClient(this);

        lobbyChannel = "Lobby";
        noticeChannel = "System";
    }

    public void Connect(string userId)
    {
        try
        {
            string sqlCommand = string.Format("U_Nickname FROM user_info WHERE U_ID='{0}'", userId);
            MySqlDataReader reader = null;
            reader = GameManager.DB.Execute(sqlCommand);

            // ������ �о��µ� ������� ������� ����
            if (reader.HasRows)
            {
                string readNick = reader["U_Nickname"].ToString();
                nickname = readNick;
                Debug.Log($"ID : {userId}, Nickname : {readNick}");
            }
            else
            {
                Debug.Log($"There is no player id[{userId}]");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0", new AuthenticationValues(nickname));
    }

    public void Update()
    {
        chatClient.Service();
    }

    public void OnApplicationQuit()
    {
        if (chatClient != null)
        {
            chatClient.Disconnect();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        if (level == DebugLevel.ERROR)
        {
            Debug.LogError(message);
        }
        else if (level == DebugLevel.WARNING)
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.Log(message);
        }
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log($"OnChatStateChange {state}");
    }

    public void OnConnected()
    {
        Debug.Log("connected server");

        chatClient.Subscribe(new string[] { noticeChannel, lobbyChannel }, 10);
    }

    public void OnDisconnected()
    {
        Debug.Log("Disconnected server");
    }

    //������Ʈ ���� chatclient.service�� ongetmessage ȣ��
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName.Equals(lobbyChannel))
        {
            OnGetLobbyMessage?.Invoke(ShowChannel(lobbyChannel));
        }
    }

    public string ShowChannel(string channelName)
    {
        if (string.IsNullOrEmpty(channelName))
        {
            return null ;
        }

        ChatChannel channel = null;
        bool found = this.chatClient.TryGetChannel(channelName, out channel);
        if (!found)
        {
            Debug.Log("ShowChannel failed to find channel: " + channelName);
            return null;
        }
        return channel.ToStringMessages();
    }



    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log($"OnPrivateMessage : {message}");
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log($"status : {user} is {status}, Msg : {message}");
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }


    // �Է��� ä���� ������ �����Ѵ�.
    public void SendChatMessage(string inputLine)
    {
        if (string.IsNullOrEmpty(inputLine))
        {
            return;
        }
        this.chatClient.PublishMessage(lobbyChannel, inputLine);
    }

    public void UnSubscribe(string[] channel)
    {
        this.chatClient.Unsubscribe(channel);
    }
}