using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class ServerManager : MonoBehaviourPunCallbacks
{    // ������Ʈ �� �� �κ񿡼� �ٸ� ��������� ���ͼ� �׽�Ʈ�� ���� �ش޶�� �Ź� �׷� �� �����ϱ�
     // �ٷ� ���� ������ ���� �ϴ� ����� �뵵�� ������ֱ�. (�κ񿡼� ���� �����ؼ� ���� �� �ƴ϶� �ٷ� ���Ӿ����� �����ϸ� ����� ���)

    [SerializeField] float countdownTimer;  // �� �� ���� �ٰ��� ���� �����ϱ�
    [SerializeField] GameObject player;

    private void Start()
    {
        // Normal game Mode
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LocalPlayer.SetLoad(true);    // ���� �÷��̾�� �ε庯���� true�� ������ֱ� (��ǻ�� �ӵ����� �ٸ��ϱ�. ���� ��ǻ�ʹ� Start ȣ�� �ʰ� �Ǵ� �뿡 ���� true�� �ڴʰ� �ٲ��
        }
        // Debug game mode
        else
        {
            PhotonNetwork.LocalPlayer.NickName = $"DebugPlayer {Random.Range(1000, 10000)}";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions() { IsVisible = false };  // �ٸ� �÷��̾ ��ġ����ŷ ���ε� �������� ���� ������ �ȵǴϱ� �����������. IsVisible = false
        PhotonNetwork.JoinOrCreateRoom("DebugRoom", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(DebugGameSeupDelay());
    }

    public override void OnDisconnected(DisconnectCause cause)  // ���� ������ �������� ��
    {
        Debug.Log($"Disconnected : {cause}");
        GameManager.Scene.LoadScene("LobbyScene");   // ������ ����µ� �������� �κ������ �Ѿ�޶�� ��û�� �� ����. �׷��� SceneManager.LoadScene���� �� ���� �ٲ��
    }

    public override void OnLeftRoom()   // ���� �濡�� �������� ��
    {
        Debug.Log("Left Room");
        PhotonNetwork.LoadLevel("LobbyScene");  // �̰� �������� ������ ���� ���� �ƴϱ� ������ �������� �κ������ �Ѿ�޶�� ��û ����
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, PhotonHashtable changedProps)
    {
        // �ٸ� �÷��̾��� ������Ƽ�� true��� ���� Ȯ���� �� ����

        if (changedProps.ContainsKey("Load"))   // �ε��� �� ��� �÷��̾ �ε��� Ŀ����������Ƽ�� true������ Ȯ�����ָ� ��.
        {
            // ���� �÷��̾���� load�� �� �� �� ���� Ȯ���ؾ���
            // ��� �÷��̾� �ε� �Ϸ�
            if (PlayerLoadCount() == PhotonNetwork.PlayerList.Length)
            {
                if (PhotonNetwork.IsMasterClient)   // ������ ��쿡��
                {
                    // ��� ���� Ÿ�ֿ̹� ���ӽ����� �Ǿ�� �ϴϱ�, �Ȱ��� Ÿ�ֿ̹� �����ϱ� ���ؼ�, �츮���� 4�� 10�� 1�ʿ� �����Ѵ�! �̷������� ����
                    PhotonNetwork.CurrentRoom.SetLoadTime(PhotonNetwork.ServerTimestamp);   // �����ð����� ����(��ǻ�͸��� �ð��� ���� ���̳� �� �����ϱ�) 
                                                                                            // �� ������ ���常 �ؾ���
                }
            }
            // �Ϻ� �÷��̾� �ε� �Ϸ�
            else
            {
                // �ٸ� �÷��̾� �ε� �� ������ ���
                Debug.Log($"Wait Players {PlayerLoadCount()} / {PhotonNetwork.PlayerList.Length}");     // �� ��� �߿� �� ���� �ε� �ƴ�
            }
        }
    }

    public override void OnRoomPropertiesUpdate(PhotonHashtable propertiesThatChanged)    // ������ �ð��� ��������. ���� �츮 ���� �ð��� �������� �׷��� �ݹ��Լ�
    {
        // �濡�� ���� �ð��� ��������
        if (propertiesThatChanged.ContainsKey("LoadTime"))
        {
            StartCoroutine(GameStartTimer());
        }
    }

    IEnumerator GameStartTimer()
    {
        int loadTime = PhotonNetwork.CurrentRoom.GetLoadTime();

        while (countdownTimer > (PhotonNetwork.ServerTimestamp - loadTime) / 1000f)     // 1000f �� ������ ������ �����ð��� �и��������. 
        {
            // countdowntimer �� �������� ������ ��� ����
            int remainTime = (int)(countdownTimer - (PhotonNetwork.ServerTimestamp - loadTime) / 1000f);
            yield return new WaitForEndOfFrame();
        }
        GameStart();

        yield return new WaitForSeconds(1f);
    }

    void GameStart()    // ����� ���� �׳� �������� ��
    {
        // TODO : Game start
    }

    IEnumerator DebugGameSeupDelay()
    {
        // �������� �����ð� 1�� ��ٷ��ֱ�
        yield return new WaitForSeconds(1f);    // 1�ʸ� ��ٷȴٰ� GameStart �ϵ���. ��Ʈ��ũ�� ��� ������ �ֱ� ���ؼ�!
        DebugGameStart();
    }

    void DebugGameStart()
    {
        float angularStart = (360.0f / 8f) * PhotonNetwork.LocalPlayer.GetPlayerNumber();
        float x = 20.0f * Mathf.Sin(angularStart * Mathf.Deg2Rad);
        float z = 20.0f * Mathf.Cos(angularStart * Mathf.Deg2Rad);
        Vector3 position = new Vector3(x, 0.0f, z);
        Quaternion rotation = Quaternion.Euler(0.0f, angularStart, 0.0f);

        // PlayerController controller = Resources.Load<PlayerController>("Player");
        // Instantiate(controller, position, rotation);  // �̰� ������ �� ���� �������� ��û���� �ʾұ� ������ ȭ�鿡 �� ĳ���͸� ����. �ٸ� �÷��̾� �� ����

        PhotonNetwork.Instantiate("ADB_PlayerAllInOne", position, rotation);    // �̰� �۵���Ű���� �÷��̾ Photon View��� ������Ʈ�� �� �ٿ�����. ������ ����ȭ �����־�� �ؼ�!!
                                                                  // Photon view : ������ ����ȭ �����ֱ�. �� �ڽ��� �������� �ٸ� �信���� ���̰� �ϴ� ��. �ڱ��ڽſ��Ը� �����ߵǴ°� ���� ���� ����並 �ִ´ٰ� �����ϸ� ��
    }

    int PlayerLoadCount()
    {
        int loadCount = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.GetLoad())
                loadCount++;
        }
        return loadCount;
    }
}