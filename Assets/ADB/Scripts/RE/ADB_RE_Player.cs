using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ADB_RE_Player : MonoBehaviourPunCallbacks
{
    // 플레이어가 가지고 있는 것들

    protected Animator anim;
    [SerializeField] protected GameObject diePlayer;
    [SerializeField] protected ADB_RE_GunName gun;
    [SerializeField] int hp;
    [SerializeField] public int Hp { get { return hp; } private set { if (hp <= 0) hp = 0; else hp = value; } }
    [SerializeField] int rank;
    [SerializeField] public int Rank { get { return rank; } set { rank = value; } }
    [SerializeField] protected KillDeathUI killDeathUI;
    public int killCount;
    public int deathCount;
    public string nickName;
    protected Player me;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        gun = GetComponentInChildren<ADB_RE_GunName>();
    }

    private void Start()
    {
        hp = 200;
        anim.Play("rifle aiming idle");
        nickName = PhotonNetwork.LocalPlayer.NickName;      // 자신의 닉네임을 저장한다.
        ADB_CustomProperty.SetKillCount(me, killCount);
    }

    protected int DecreaseHp(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            hp = 0;
        return hp;
    }

    public void PlayerSet()
    {
        photonView.Owner.SetCustomProperties(new Hashtable {{ "Rank", this.rank }});
        photonView.Owner.SetCustomProperties(new Hashtable { { "NickName", this.nickName } });
        photonView.Owner.SetCustomProperties(new Hashtable { { "KillCount", this.killCount } });
    }
}