using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    // Player�� Die�ϸ� SpawnPoint���� �������� �����Ǳ�

    GameObject player;
    Transform[] spawnPoint;     // �ʿ��� �巡��

    private void Awake()
    {
        player = GameManager.Resource.Load<GameObject>("ADB_PlayerSet");
    }

    void Spawn()
    {
        // 1. SpawnPoint�� �������� ����
        int point = Random.Range(0, spawnPoint.Length - 1);

        // 2. �÷��̾� ��ġ�� �� SpawnPoint ��ġ�� ��
        player.transform.position = spawnPoint[point].position;

        // 3. �÷��̾ Ȱ��ȭ, diePlayer ��Ȱ��ȭ
        player.SetActive(true);

        // * TODO : �ٵ� ��Ȱ�ϰ� ���� hp, źâ, �ִϸ��̼� ���� ���󺹱� ���ѳ��� ��
    }

}
