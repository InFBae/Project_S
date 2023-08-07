using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ahndabi
{
    public class Player : MonoBehaviour
    {
        // �÷��̾ ������ �ִ� �͵�

        protected Animator anim;
        [SerializeField] protected GameObject diePlayer;
        [SerializeField] protected float hp;
        [SerializeField] protected Gun gun;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            gun = GetComponentInChildren<Gun>();
        }

        private void Start()
        {
            hp = 200f;
        }
    }
}