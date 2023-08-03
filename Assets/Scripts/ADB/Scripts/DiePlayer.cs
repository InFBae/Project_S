using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePlayer : MonoBehaviour
{
    // Player�� transform�� ��� �Ȱ��ƾ� ��

    [SerializeField] GameObject Player;
    private void Start()
    {
        Player = GameManager.Resource.Load<GameObject>("ADB_VisiblePlayer 1");
    }

    private void LateUpdate()
    {
        gameObject.transform.position = Player.transform.position;
        gameObject.transform.rotation = Player.transform.rotation;
    }
}
