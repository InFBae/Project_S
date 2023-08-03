using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ahndabi
{
    // �÷��̾� ��ü�� �پ��ִ� ��ũ��Ʈ
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] Gun gun;
        Animator anim;


        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }

        void Fire()   // ���� ��
        {
            // ������ �� �ѿ� �ִ� Fire()�� ȣ���Ѵ�.
            gun.Fire();
            anim.SetTrigger("Fire");
        }

        void OnFire(InputValue value)
        {
            // ���콺 ��Ŭ���ϸ� Fire��
            Fire();
        }

    }
}