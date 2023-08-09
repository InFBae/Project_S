using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ahndabi
{
    public class SpawnPlayer : MonoBehaviour
    {
        // Player�� Die�ϸ� SpawnPoint���� �������� �����Ǳ�

        [SerializeField] GameObject player;
        [SerializeField] Transform[] spawnPoint;     // �ʿ��� �巡��

        private void Awake()
        {
            player = GameManager.Resource.Load<GameObject>("ADB_PlayerSet");

            Transform[] children = GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                for (int i = 0; i < child.childCount; ++i)
                {
                    spawnPoint[i] = child;
                }
            }
        }

        void Spawn()
        {
            // 1. SpawnPoint�� �������� ����
            int point = Random.Range(0, spawnPoint.Length - 1);

            // 2. �÷��̾� ��ġ�� �� SpawnPoint ��ġ�� ��
            player.transform.position = spawnPoint[point].transform.position;

            // 3. �÷��̾ Ȱ��ȭ, diePlayer ��Ȱ��ȭ
            player.SetActive(true);

            // * TODO : �ٵ� ��Ȱ�ϰ� ���� hp, źâ, �ִϸ��̼� ���� ���󺹱� ���ѳ��� ��
        }
    }
}
