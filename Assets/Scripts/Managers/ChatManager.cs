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

public class ChatManager : MonoBehaviour, IChatClientListener
{
    [SerializeField] private ChatClient chatClient;
    [SerializeField] private string userName;
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

    public void Connect(string username)
    {
        this.userName = username;
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0", new AuthenticationValues(userName));
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

    //업데이트 마다 chatclient.service가 ongetmessage 호출
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


    // 입력한 채팅을 서버로 전송한다.
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