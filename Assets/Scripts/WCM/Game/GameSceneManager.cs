using Photon.Pun;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun.UtilityScripts;
using System.Collections;
using ExitGames.Client.Photon.Encryption;
using JetBrains.Annotations;

public class GameSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text infoText;
    [SerializeField] float countdownTime;
    [SerializeField] float killCount;
    [SerializeField] Transform spawnPoint;

    Transform[] spawnPointArray = new Transform[7];

    private void Start()
    {
        //����׸�� ���ุ���
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LocalPlayer.SetLoad(true);
        }
        else
        {
            infoText.text = "Debug Mode";
            PhotonNetwork.LocalPlayer.NickName = $"DebugPlayer {Random.Range(100, 1000)}";
            PhotonNetwork.ConnectUsingSettings();
        }

        //������ġ ����
        for (int i = 0; i < spawnPoint.childCount; i++)
        {
            spawnPointArray[i] = spawnPoint.GetChild(i).transform;
        }
    }

    //����׸�� ���ӽ� �ٷ� ������ ��������
    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions() { IsVisible = true };
        PhotonNetwork.JoinOrCreateRoom("DebugRoom", options, TypedLobby.Default);
    }

    //�濡������? setupdelay����
    public override void OnJoinedRoom()
    {
        StartCoroutine(DebugGameSetupDelay());
    }

    //1�ʱ�ٸ��� ����� ���� ��ŸƮ ����
    IEnumerator DebugGameSetupDelay()
    {
        yield return new WaitForSeconds(1f);
        DebugGamestart();
    }

    //������ ���� ���� �ϴ� �÷��̾ ��ȯ�ؾ���.  
    private void DebugGamestart()
    {
        //Transform position = new Vector3(spawnPointArray[Random.Range(0, 7)]);

        Vector3 spawnPosition = spawnPointArray[Random.Range(0, 7)].position;
        spawnPosition = Vector3.zero;

        PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity, 0);

        // ������ Ŭ���̾�Ʈ ����
        if (PhotonNetwork.IsMasterClient)
        {
            //�������� �۾� ������
        }
    }

    //����ġ �ʰ� ������ �������� ������ ������, ���� ������ �Űܹ���
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnect : {cause}");
        GameManager.Scene.LoadScene("RoomScene_");
    }

    //���� ������ �κ�� ��������
    public override void OnLeftRoom()
    {
        Debug.Log("LeftRoom");
        PhotonNetwork.LoadLevel("LobbyScene_");
    }

    //�����Ͱ� ��ü������ ? -> ������Ŭ���̾�Ʈ�� ��������
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(newMasterClient.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            //�����Ͱ� �ϴ� �۾� ����
            //�츮�� ������
        }
    }

    //�ε��� �÷��̾� �� ����
    private int PlayerLoadCount()
    {
        int loadCount = 0;
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if(player.GetLoad())
                loadCount++;
        }
        return loadCount;
    }
    
    //�ϴ� ������ �ʿ����
   /* public override void OnRoomPropertiesUpdate(PhotonHashtable propertiesThatChanged)
    {

    }

    //
    IEnumerator GameStartTimer()
    {
        double loadTime = PhotonNetwork.CurrentRoom.GetLoadTime();
        while (countdownTime > PhotonNetwork.Time - loadTime)
        {
            int remainTime = (int)(countdownTime - (PhotonNetwork.Time - loadTime));
            infoText.text = $"All Player Loaded, Start count down : {remainTime + 1}";
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Game Start!");
        infoText.text = "Game Start!";
        GameStart();

        yield return new WaitForSeconds(1f);
        infoText.text = "";
    }*/


}
