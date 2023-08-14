using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ahndabi;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using JBB;

public class ADB_RE_Player : MonoBehaviourPunCallbacks
{
    // �÷��̾ ������ �ִ� �͵�

    protected Animator anim;
    [SerializeField] protected GameObject diePlayer;
    protected ADB_RE_GunName gun;
    [SerializeField] int hp = 200;
    [SerializeField] public int Hp { get { return hp; } private set { if (hp <= 0) hp = 0; else hp = value; } }
    [SerializeField] int rank;
    [SerializeField] public int Rank { get { return rank; } set { rank = value; } }
    public int killCount;
    public int deathCount;
    public string nickName;
    protected Photon.Realtime.Player player;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        gun = GetComponentInChildren<ADB_RE_GunName>();
    }

    private void OnEnable()
    {
        KillManager.OnKilled.AddListener(IncreaseKillCount);
        ahndabi.StatusUI.OnHPChanged?.Invoke(Hp);
    }

    private void Start()
    {
        anim.Play("rifle aiming idle");
        nickName = PhotonNetwork.LocalPlayer.NickName;      // �ڽ��� �г����� �����Ѵ�.
    }

    protected int DecreaseHp(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            hp = 0;
        return hp;
    }

    public void IncreaseKillCount(Photon.Realtime.Player Killed, Photon.Realtime.Player dead, bool headShot)      // ���λ��, �������, ��弦����
    {
        ++killCount;
        Killed.SetKillCount(killCount);
    }
}