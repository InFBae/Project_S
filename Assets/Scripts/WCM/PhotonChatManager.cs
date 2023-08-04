using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PhotonChatManager : MonoBehaviour
{
/*
    [SerializeField] 
    private ChatClient chatClient;
    private bool isConnected;

    [SerializeField] string username;
    [SerializeField] string userID;

    public void UsernameOnValueChange(string valueIn)
    {
        username = valueIn;
    }
    public void ChatConnect()
    {
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "kr";
        chatClient.Connect(this.appId, "1.0", this.UserName, null);
    }
    private string currentChannelName;

    [SerializeField] TMP_InputField chatInputField;
    [SerializeField] TMP_Text chatDisplay;



    private void Start()
    {
    }

    private void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        throw new System.NotImplementedException();
    }


    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs += senders[i] + "=" + messages[i] + ", ";
        }
        Concole.wri
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnConnected()
    {
        throw new System.NotImplementedException();
    }*/
}
