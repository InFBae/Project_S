using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADB_RE_DiePlayer : MonoBehaviourPun
{
    // Player의 transform와 계속 똑같아야 함

    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Destroy(gameObject, 5f);
    }

}
