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
        //디버그모드 실행만들기
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

        //스폰위치 저장
        for (int i = 0; i < spawnPoint.childCount; i++)
        {
            spawnPointArray[i] = spawnPoint.GetChild(i).transform;
        }
    }

    //디버그모드 접속시 바로 방으로 보내버림
    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions() { IsVisible = true };
        PhotonNetwork.JoinOrCreateRoom("DebugRoom", options, TypedLobby.Default);
    }

    //방에들어오면? setupdelay실행
    public override void OnJoinedRoom()
    {
        StartCoroutine(DebugGameSetupDelay());
    }

    //1초기다리고 디버그 게임 스타트 실행
    IEnumerator DebugGameSetupDelay()
    {
        yield return new WaitForSeconds(1f);
        DebugGamestart();
    }

    //디버깅용 게임 실행 일단 플레이어를 소환해야함.  
    private void DebugGamestart()
    {
        //Transform position = new Vector3(spawnPointArray[Random.Range(0, 7)]);

        Vector3 spawnPosition = spawnPointArray[Random.Range(0, 7)].position;
        spawnPosition = Vector3.zero;

        PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity, 0);

        // 마스터 클라이언트 동작
        if (PhotonNetwork.IsMasterClient)
        {
            //마스터의 작업 총판정
        }
    }

    //예기치 않게 연결이 끊겼을때 원인을 보내고, 씬을 룸으로 옮겨버림
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnect : {cause}");
        GameManager.Scene.LoadScene("RoomScene_");
    }

    //룸을 나가면 로비로 보내버림
    public override void OnLeftRoom()
    {
        Debug.Log("LeftRoom");
        PhotonNetwork.LoadLevel("LobbyScene_");
    }

    //마스터가 교체됬을떄 ? -> 마스터클라이언트가 나갔을떄
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(newMasterClient.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            //마스터가 하던 작업 실행
            //우리는 총판정
        }
    }

    //로딩된 플레이어 수 세기
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
    
    //일단 지금은 필요없음
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
