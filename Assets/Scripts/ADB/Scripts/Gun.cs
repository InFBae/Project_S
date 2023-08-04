using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace ahndabi
{
    public abstract class Gun : MonoBehaviour
    {
        public UnityEvent OnFire;
        [SerializeField] protected Animator anim;
        [SerializeField] protected Transform muzzlePos;
        [SerializeField] protected float fireDamage;
        [SerializeField] protected int allBullet;             // 총탄 개수
        [SerializeField] protected int availableBullet;       // 재장전 하기 전 사용 가능한 총탄 개수 
        [SerializeField] protected int curAvailavleBullet;    // 계속 사용 중인 현재 총알 개수   
        

        private void Awake()
        {
            // anim = GetComponentInParent<Animator>();
            // 플레이어라는 게임오브젝트를 불러와서 그 플레이어의 GetComponent<애니메이터>로 하면 됨.
        }

        private void Start()
        {
        }

        public void ForwardDirection()
        {
            // 총이 카메라와 같은 방향을 바라보도록
            // transform.localRotation = Camera.main.transform.localRotation;
        }

        public abstract void Fire();        // 총마다 Fire 방식이 다르니까 abstract로
        public abstract void Reload();   // 재장전
    }
}
