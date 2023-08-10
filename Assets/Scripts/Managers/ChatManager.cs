using ExitGames.Client.Photon;
using Photon.Chat;
using System;
using UnityEngine;
using Photon.Pun;
using AuthenticationValues = Photon.Chat.AuthenticationValues;
using UnityEngine.Events;
using MySql.Data.MySqlClient;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    [SerializeField] private ChatClient chatClient;
    [SerializeField] private string nickname;
    private string lobbyChannel;
    private string noticeChannel;

    public static UnityEvent<string> OnGetLobbyMessage = new UnityEvent<string>();
    public static UnityEvent<string, int> OnFriendStatusChanged = new UnityEvent<string, int>();
    public static UnityEvent OnFriendListChanged = new UnityEvent();

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
            string sqlCommand = string.Format("SELECT U_Nickname FROM user_info WHERE U_ID='{0}'", userId);
            MySqlDataReader reader = null;
            reader = GameManager.DB.Execute(sqlCommand);

            // 리더가 읽었는데 있을경우 없을경우 구분
            if (reader.HasRows)
            {
                reader.Read();
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

    public void DisConnect()
    {
        chatClient.Disconnect();
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

        AddDBFriends();
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
            for (int i = 0; i < senders.Length; i++)
            {
                OnGetLobbyMessage?.Invoke($"{senders[i]} : {messages[i]}");
            }
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log($"OnPrivateMessage : {message}");
    }
    void IChatClientListener.OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log($"status : {user} is {status}, Msg : {message}");
        OnFriendStatusChanged?.Invoke(user, status);
    }
    public void OnSubscribed(string[] channels, bool[] results)
    {

    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnUserSubscribed(string channel, string user)
    {

    }

    public void OnUserUnsubscribed(string channel, string user)
    {

    }


    // 입력한 채팅을 서버로 전송한다.
    public void SendChatMessage(string inputLine)
    {
        if (string.IsNullOrEmpty(inputLine))
        {
            return;
        }
        chatClient.PublishMessage(lobbyChannel, inputLine);
    }

    public void UnSubscribe(string[] channel)
    {
        chatClient.Unsubscribe(channel);
    }

    public bool AddFriend(string nickname)
    {
        string[] nicknames = new string[] { nickname };
        if (chatClient.AddFriends(nicknames))
        {
            Debug.Log($"Succeed AddFriend {nickname}");
            return true;
        }
        Debug.Log($"Failed AddFriend {nickname}");
        return false;
    }

    public bool RemoveFriend(string nickname)
    {
        string[] nicknames = new string[] { nickname };
        if (chatClient.RemoveFriends(nicknames))
        {
            Debug.Log($"Succeed RemoveFriend {nickname}");
            return true;
        }
        Debug.Log($"Failed RemoveFriend {nickname}");
        return false;
    }

    public void AddDBFriends()
    {
        string sqlCommand = $"SELECT * FROM friend_info WHERE Owner = '{PhotonNetwork.LocalPlayer.NickName}'";
        MySqlDataReader reader = null;
        reader = GameManager.DB.Execute(sqlCommand);

        if (reader.HasRows)
        {
            reader.Read();
            for (int i = 0; i < 10; i++)
            {
                string nickname = reader[$"friend{i + 1}"].ToString();
                if (nickname != "")
                {
                    AddFriend(nickname);
                }
            }
        }
    }

}