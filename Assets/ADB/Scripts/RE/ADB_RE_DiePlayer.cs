using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADB_RE_DiePlayer : MonoBehaviour
{
    // Player의 transform와 계속 똑같아야 함

    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Destroy(gameObject.transform.parent.gameObject, 5f);
    }

}
