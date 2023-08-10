using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ahndabi
{
    public class SpawnPlayer : MonoBehaviour
    {
        // Player�� Die�ϸ� SpawnPoint���� �������� �����Ǳ�

        [SerializeField] GameObject Player;
        [SerializeField] Transform spawnPoint;     // �ʿ��� �巡��
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
            // 1. SpawnPoint�� �������� ����
            int point = Random.Range(0, spawnPoints.Length - 1);

            // 2. �÷��̾� ��ġ�� �� SpawnPoint ��ġ�� ��
            Player.transform.position = spawnPoints[point].transform.position;
            Player.transform.forward = spawnPoints[point].transform.forward;

            // 3. �÷��̾ Ȱ��ȭ, diePlayer ��Ȱ��ȭ
            Player.SetActive(true);

            // * TODO : �ٵ� ��Ȱ�ϰ� ���� hp, źâ, �ִϸ��̼� ���� ���󺹱� ���ѳ��� ��
        }
    }
}
