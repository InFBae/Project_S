using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    // Player가 Die하면 SpawnPoint에서 랜덤으로 생성되기

    GameObject player;
    Transform[] spawnPoint;     // 맵에서 드래그

    private void Awake()
    {
        player = GameManager.Resource.Load<GameObject>("ADB_PlayerSet");
    }

    void Spawn()
    {
        // 1. SpawnPoint가 랜덤으로 지정
        int point = Random.Range(0, spawnPoint.Length - 1);

        // 2. 플레이어 위치를 그 SpawnPoint 위치로 함
        player.transform.position = spawnPoint[point].position;

        // 3. 플레이어를 활성화, diePlayer 비활성화
        player.SetActive(true);

        // * TODO : 근데 부활하고 나면 hp, 탄창, 애니메이션 등을 원상복구 시켜놔야 함
    }

}
