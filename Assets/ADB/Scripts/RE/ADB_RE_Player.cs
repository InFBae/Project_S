using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ADB_RE_Player : MonoBehaviour
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