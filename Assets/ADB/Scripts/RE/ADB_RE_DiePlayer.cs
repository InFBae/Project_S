using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADB_RE_DiePlayer : MonoBehaviour
{
    // Player�� transform�� ��� �Ȱ��ƾ� ��

    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Destroy(gameObject.transform.parent.gameObject, 5f);
    }

}
