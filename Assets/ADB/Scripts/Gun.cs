using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace ahndabi
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected Transform muzzlePos;
        [SerializeField] protected int fireDamage;
        [SerializeField] protected int remainBullet;             // 총탄 개수
        [SerializeField] protected int availableBullet;       // 재장전 하기 전 사용 가능한 총탄 개수 
        [SerializeField] protected int curAvailavleBullet;    // 계속 사용 중인 현재 총알 개수   
        [SerializeField] protected StatusUI statusUI;

        public void ForwardDirection()
        {
            // 총이 카메라와 같은 방향을 바라보도록
            // transform.localRotation = Camera.main.transform.localRotation;
        }

        public abstract void Fire();        // 총마다 Fire 방식이 다르니까 abstract로
        public abstract void Reload();   // 재장전
    }
}
