using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADB_RE_DiePlayer : MonoBehaviourPun
{
    // Player�� transform�� ��� �Ȱ��ƾ� ��

    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Destroy(gameObject, 5f);
    }

}
