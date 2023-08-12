using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ahndabi;

public class PlayerController : MonoBehaviour
{
    PhotonView PV;
    PlayerAttacker PAttack;
    PlayerMover PMover;
    PlayerAimMove PAimMove;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        PAttack = GetComponentInChildren<PlayerAttacker>();
        PMover = GetComponentInChildren<PlayerMover>();
        PAimMove = GetComponentInChildren<PlayerAimMove>();
    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            PAttack.enabled = false;
            PMover.enabled = false;
            PAimMove.enabled = false;
        }
    }
}
