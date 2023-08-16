using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADB_RE_DiePlayer : MonoBehaviour
{
    // Player�� transform�� ��� �Ȱ��ƾ� ��

    [SerializeField] Transform Player;

    private void Awake()
    {
        //gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        gameObject.transform.position = Player.transform.position;
        gameObject.transform.rotation = Player.transform.rotation;
        Destroy(gameObject.transform.parent.gameObject, 5f);
    }

}
