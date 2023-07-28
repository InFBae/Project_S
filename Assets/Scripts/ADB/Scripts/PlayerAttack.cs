using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ahndabi
{
    // 플레이어 본체에 붙어있는 스크립트
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] Gun gun;

        void Fire()   // 총을 쏨
        {
            // 공격할 땐 총에 있는 Fire()를 호출한다.
            gun.Fire();
        }

        void OnFire(InputValue value)
        {
            // 마우스 좌클릭하면 Fire됨
            Fire();
        }
    }
}