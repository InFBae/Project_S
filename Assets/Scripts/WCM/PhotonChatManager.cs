using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using TMPro;
using UnityEngine;
using System;
using AuthenticationValues = Photon.Chat.AuthenticationValues;
using Photon.Chat.Demo;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient chatClient;
    private string userName;
    private string currentChannelName;

    public TMP_InputField ChatInputField;
    public TMP_Text currentChannelText;
    public TMP_Text outputText;

    protected internal ChatAppSettings chatAppSettings;
    public ChatAppSettings ChatAppSettings
    {
        get { return this.chatAppSettings; }
    }
    private void Start()
    {
        Application.runInBackground = true;

        //�׽�Ʈ�� �ð����� ������������
        userName = DateTime.Now.ToShortTimeString();
        currentChannelName = "Channel 001";

        chatClient = new ChatClient(this);

        this.chatAppSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();


        bool appIdPresent = !string.IsNullOrEmpty(this.chatAppSettings.AppIdChat);
        // ��׶���� ���� ����
        //chatClient.UseBackgroundWorkerForSending = true;
        this.chatClient.AuthValues = new AuthenticationValues(this.userName);
        this.chatClient.ConnectUsingSettings(this.chatAppSettings);


        Debug.Log("Connecting as: " + this.userName);


        //chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0", new AuthenticationValues(userName));
        Debug.Log($"connecting {userName}");
    }

    public void AddLine(string lineString)
    {
        outputText.text += lineString + "\r\n";
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
        AddLine("connected server");

        chatClient.Subscribe(new string[] { currentChannelName }, 10);
    }

    public void OnDisconnected()
    {
        AddLine("disconnected server");
    }

    //������Ʈ ���� chatclient.service�� ongetmessage ȣ��
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName.Equals(currentChannelName))
        {
            //update text
            this.ShowChannel(currentChannelName);
        }
    }

    public void ShowChannel(string channelName)
    {
        if (string.IsNullOrEmpty(channelName))
        {
            return;
        }

        ChatChannel channel = null;
        bool found = this.chatClient.TryGetChannel(channelName, out channel);
        if (!found)
        {
            Debug.Log("ShowChannel failed to find channel: " + channelName);
            return;
        }

        this.currentChannelName = channelName;
        // ä�ο� ���� �� ��� ä�� �޼����� �ҷ��´�.
        // ���� �̸��� ä�� ������ �Ѳ����� �ҷ�������.
        this.currentChannelText.text = channel.ToStringMessages();
        Debug.Log("ShowChannel: " + currentChannelName);
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
        AddLine($"channel entered , {channels}");
    }

    public void OnUnsubscribed(string[] channels)
    {
        AddLine($"channel left , {channels}");
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnEnterSend()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            this.SendChatMessage(this.ChatInputField.text);
            this.ChatInputField.text = "";
        }
    }

    // �Է��� ä���� ������ �����Ѵ�.
    private void SendChatMessage(string inputLine)
    {
        if (string.IsNullOrEmpty(inputLine))
        {
            return;
        }

        this.chatClient.PublishMessage(currentChannelName, inputLine);
    }
}