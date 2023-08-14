using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class RE_Player : MonoBehaviourPunCallbacks
{
    // �÷��̾ ������ �ִ� �͵�

    protected Animator anim;
    [SerializeField] protected GameObject diePlayer;
    [SerializeField] protected float hp;
    [SerializeField] protected RE_GunName gun;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        gun = GetComponentInChildren<RE_GunName>();
    }

    private void Start()
    {
        hp = 200f;
    }
}