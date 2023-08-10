using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ADB_RE_Player : MonoBehaviour
{
    // 플레이어가 가지고 있는 것들

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