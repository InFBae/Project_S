using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace ahndabi
{
    public class Player : MonoBehaviour
    {
        // 플레이어가 가지고 있는 것들
        [SerializeField] protected Animator anim;
        [SerializeField] protected GameObject diePlayer;
        [SerializeField] protected Gun gun;
        [SerializeField] int hp;
        [SerializeField] public int Hp { get { return hp; } private set { if (hp <= 0) hp = 0; else hp = value; } }
        [SerializeField] protected int killCount;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            gun = GetComponentInChildren<Gun>();
        }

        private void OnEnable()
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
}