using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ahndabi
{
    public class SpawnPlayer : MonoBehaviour
    {
        // Player가 Die하면 SpawnPoint에서 랜덤으로 생성되기

        [SerializeField] GameObject Player;
        [SerializeField] Transform spawnPoint;     // 맵에서 드래그
        [SerializeField] Transform[] spawnPoints;


        private void Awake()
        {
            int spawnPointCount = spawnPoint.childCount;
            spawnPoints = new Transform[spawnPointCount];

            for (int i = 0; i < spawnPointCount; i++)
            {
                spawnPoints[i] = spawnPoint.GetChild(i).transform;
            }
        }

        public void Spawn()
        {
            // 1. SpawnPoint가 랜덤으로 지정
            int point = Random.Range(0, spawnPoints.Length - 1);

            // 2. 플레이어 위치를 그 SpawnPoint 위치로 함
            Player.transform.position = spawnPoints[point].transform.position;
            Player.transform.forward = spawnPoints[point].transform.forward;

            // 3. 플레이어를 활성화, diePlayer 비활성화
            Player.SetActive(true);

            // * TODO : 근데 부활하고 나면 hp, 탄창, 애니메이션 등을 원상복구 시켜놔야 함
        }
    }
}
