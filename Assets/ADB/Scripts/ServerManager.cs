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
{    // 프로젝트 할 때 로비에서 다른 사람들한테 들어와서 테스트좀 같이 해달라고 매번 그럴 수 없으니까
     // 바로 게임 씬으로 들어가서 하는 디버깅 용도를 만들어주기. (로비에서 정상 접속해서 들어가는 게 아니라 바로 게임씬에서 시작하면 디버깅 모드)

    [SerializeField] float countdownTimer;  // 몇 초 세고 다같이 게임 시작하기
    [SerializeField] GameObject player;

    private void Start()
    {
        // Normal game Mode
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LocalPlayer.SetLoad(true);    // 들어온 플레이어마다 로드변수를 true로 만들어주기 (컴퓨터 속도마다 다르니까. 느린 컴퓨터는 Start 호출 늦게 되는 대에 따라 true로 뒤늦게 바뀌도록
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
        RoomOptions options = new RoomOptions() { IsVisible = false };  // 다른 플레이어가 매치매이킹 중인데 디버깅룸을 보고 들어오면 안되니까 비공개방으로. IsVisible = false
        PhotonNetwork.JoinOrCreateRoom("DebugRoom", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(DebugGameSeupDelay());
    }

    public override void OnDisconnected(DisconnectCause cause)  // 만약 연결이 끊어졌을 때
    {
        Debug.Log($"Disconnected : {cause}");
        GameManager.Scene.LoadScene("LobbyScene");   // 접속이 끊겼는데 서버에게 로비씬으로 넘어가달라고 요청할 순 없지. 그래서 SceneManager.LoadScene으로 내 씬만 바뀌도록
    }

    public override void OnLeftRoom()   // 만약 방에서 나가졌을 때
    {
        Debug.Log("Left Room");
        PhotonNetwork.LoadLevel("LobbyScene");  // 이건 서버에서 접속이 끊긴 것이 아니기 때문에 서버에세 로비씬으로 넘어가달라고 요청 가능
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, PhotonHashtable changedProps)
    {
        // 다른 플레이어의 프로퍼티가 true라는 것을 확인할 수 있음

        if (changedProps.ContainsKey("Load"))   // 로드일 때 모든 플레이어가 로드라는 커스텀프로퍼티가 true인지를 확인해주면 됨.
        {
            // 지금 플레이어들의 load가 총 몇 개 인지 확인해야함
            // 모든 플레이어 로딩 완료
            if (PlayerLoadCount() == PhotonNetwork.PlayerList.Length)
            {
                if (PhotonNetwork.IsMasterClient)   // 방장인 경우에만
                {
                    // 모두 같은 타이밍에 게임시작이 되어야 하니까, 똑같은 타이밍에 시작하기 위해선, 우리방은 4시 10분 1초에 시작한다! 이런식으로 설정
                    PhotonNetwork.CurrentRoom.SetLoadTime(PhotonNetwork.ServerTimestamp);   // 서버시간으로 지정(컴퓨터마다 시간이 조금 차이날 수 있으니까) 
                                                                                            // 위 설정은 방장만 해야함
                }
            }
            // 일부 플레이어 로딩 완료
            else
            {
                // 다른 플레이어 로딩 될 때까지 대기
                Debug.Log($"Wait Players {PlayerLoadCount()} / {PhotonNetwork.PlayerList.Length}");     // 총 몇명 중에 몇 명이 로딩 됐다
            }
        }
    }

    public override void OnRoomPropertiesUpdate(PhotonHashtable propertiesThatChanged)    // 서버에 시간이 정해졌다. 현재 우리 방의 시간이 정해졌다 그러면 콜백함수
    {
        // 방에서 시작 시간이 정해졌다
        if (propertiesThatChanged.ContainsKey("LoadTime"))
        {
            StartCoroutine(GameStartTimer());
        }
    }

    IEnumerator GameStartTimer()
    {
        int loadTime = PhotonNetwork.CurrentRoom.GetLoadTime();

        while (countdownTimer > (PhotonNetwork.ServerTimestamp - loadTime) / 1000f)     // 1000f 을 나누는 이유는 서버시간이 밀리세컨드라서. 
        {
            // countdowntimer 가 남아있을 때까지 계속 진행
            int remainTime = (int)(countdownTimer - (PhotonNetwork.ServerTimestamp - loadTime) / 1000f);
            yield return new WaitForEndOfFrame();
        }
        GameStart();

        yield return new WaitForSeconds(1f);
    }

    void GameStart()    // 디버그 말고 그냥 시작했을 때
    {
        // TODO : Game start
    }

    IEnumerator DebugGameSeupDelay()
    {
        // 서버에게 여유시간 1초 기다려주기
        yield return new WaitForSeconds(1f);    // 1초만 기다렸다가 GameStart 하도록. 네트워크에 잠깐 여유를 주기 위해서!
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
        // Instantiate(controller, position, rotation);  // 이건 생성만 한 거지 서버에게 요청하지 않았기 때문에 화면에 내 캐릭터만 보임. 다른 플레이어 안 보임

        PhotonNetwork.Instantiate("ADB_PlayerAllInOne", position, rotation);    // 이걸 작동시키려면 플레이어에 Photon View라는 컴포넌트를 꼭 붙여야함. 서버에 동기화 시켜주어야 해서!!
                                                                  // Photon view : 서버에 동기화 시켜주기. 즉 자신의 정보들을 다른 뷰에서도 보이게 하는 것. 자기자신에게만 보여야되는거 빼고 전부 포톤뷰를 넣는다고 생각하면 됨
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