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

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            hp = 200f;
        }
    }
}