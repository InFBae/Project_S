using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class KillManager : MonoBehaviourPunCallbacks
{
    public static UnityEvent<Player, Player, bool> OnKilled = new UnityEvent<Player, Player, bool>();        // 죽인 사람, 죽은 사람, 헤드샷

    private void OnEnable()
    {
        // 이벤트 추가

    }
}    