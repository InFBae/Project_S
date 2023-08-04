using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ahndabi
{
    public class Player : MonoBehaviour
    {
        // 플레이어가 가지고 있는 것들

        protected Animator anim;
        [SerializeField] protected GameObject diePlayer;
        [SerializeField] protected float hp;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            hp = 200f;
        }
    }
}