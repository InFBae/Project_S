using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class KillManager : MonoBehaviourPunCallbacks
{
    public static UnityEvent<Player, Player, bool> OnKilled = new UnityEvent<Player, Player, bool>();        // ���� ���, ���� ���, ��弦

    private void OnEnable()
    {
        // �̺�Ʈ �߰�

    }
}    