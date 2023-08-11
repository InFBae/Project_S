using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ADB_RE_Player : MonoBehaviour
{
    // 플레이어가 가지고 있는 것들

    protected Animator anim;
    [SerializeField] protected GameObject diePlayer;
    [SerializeField] protected ADB_RE_GunName gun;
    [SerializeField] int hp;
    [SerializeField] public int Hp { get { return hp; } private set { if (hp <= 0) hp = 0; else hp = value; } }
    [SerializeField] protected KillDeathUI killDeathUI;
    public int killCount;
    public int deathCount;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        gun = GetComponentInChildren<ADB_RE_GunName>();
    }

    private void Start()
    {
        hp = 200;
        anim.Play("rifle aiming idle");
    }

    protected int DecreaseHp(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            hp = 0;
        return hp;
    }
}