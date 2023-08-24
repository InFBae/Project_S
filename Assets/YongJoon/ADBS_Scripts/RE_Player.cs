using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class RE_Player : MonoBehaviourPunCallbacks
{
    // 플레이어가 가지고 있는 것들
    [SerializeField] public JBB.StatusUI statusUI;
    [SerializeField] public Animator anim;
    [SerializeField] public RE_GunName gun;
    [SerializeField] int hp = 200;
    [SerializeField] public int Hp { get { return hp; } private set 
        { if (hp <= 0) 
                hp = 0; 
            else 
                hp = value; 
            statusUI.OnHPChanged?.Invoke(hp);
        } }
    [SerializeField] int rank;
    [SerializeField] public int Rank { get { return rank; } set { rank = value; } }
    public int killCount;
    public int deathCount;
    public string nickName;
    public Photon.Realtime.Player player;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        gun = GetComponentInChildren<RE_GunName>();        
    }

    public override void OnEnable()
    {
        base.OnEnable();

        anim = GetComponentInChildren<Animator>();
        gun = GetComponentInChildren<RE_GunName>();
    }

    private void Start()
    {
        anim.Play("rifle aiming idle");
        nickName = PhotonNetwork.LocalPlayer.NickName;      // 자신의 닉네임을 저장한다.
        statusUI.OnHPChanged?.Invoke(Hp);
    }

    public void DecreaseHp(int damage)
    {
        Hp -= damage;
    }
    public void ResetHp()
    {
        hp = 200;
        statusUI.OnHPChanged?.Invoke(Hp);
    }

}