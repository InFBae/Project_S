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

        void Fire()   // ���� ��
        {
            // ������ �� �ѿ� �ִ� Fire()�� ȣ���Ѵ�.
            gun.Fire();
        }

        void OnFire(InputValue value)
        {
            // ���콺 ��Ŭ���ϸ� Fire��
            Fire();
        }
    }
}