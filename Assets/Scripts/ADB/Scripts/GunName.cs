using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace ahndabi
{
    public class GunName : Gun
    {
        [SerializeField] GameObject hitParticle;
        [SerializeField] ParticleSystem bloodParticle;
        [SerializeField] TrailRenderer trailEffect;
        [SerializeField] CinemachineRecomposer camera;
        [SerializeField] float maxDistance;     // 최대 사거리. 60
        [SerializeField] float bulletSpeed;
        [SerializeField] float fireCoolTime;        // 연발 나가는 쿨타임
        float timer = 0f;
        bool isMousePress = false;

        private void Awake()
        {
            bloodParticle = GameManager.Resource.Load<ParticleSystem>("BloodParticle");
            hitParticle = GameManager.Resource.Load<GameObject>("HitEffect");
            trailEffect = GameManager.Resource.Load<TrailRenderer>("BulletTrail");
        }

        private void Start()
        {
            allBullet = 1000;
            availableBullet = 60;
            fireDamage = 20;
            curAvailavleBullet = availableBullet;
        }

        private void Update()
        {
            Debug.DrawRay(muzzlePos.transform.position, Camera.main.transform.forward * maxDistance, Color.green);
            ContinueousFire();      // 연발

            // ForwardDirection();     // 총이 카메라 가운데 위치를 바라보도록
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        void ContinueousFire()      // 연발
        {
            timer += Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                isMousePress = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isMousePress = false;
            }

            if (isMousePress)
            {
                if(timer >= fireCoolTime)
                {
                    Fire();
                    timer = 0f;
                }
            }
        }

        public override void Fire()
        {
            RaycastHit hit;

            if (curAvailavleBullet == 0)   // 총알 없으면 쏘지 못하도록
                return;

            // TODO : Relaoding 애니메이션 중에는 쏘지 못하도록
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("reloading"))
                return;

            --curAvailavleBullet;

            // 레이캐스트를 솼는데 부딪힌 물체가 있다면
            if (Physics.Raycast(muzzlePos.transform.position, Camera.main.transform.forward, out hit, maxDistance))
            {
                if (hit.transform.gameObject.layer == 7)  // 바디 레이어를 맞췄다면?
                {
                    hit.transform.gameObject.GetComponentInParent<PlayerTakeDamage>().TakeDamage(fireDamage);
                    ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                    Debug.Log("바디");
                }
                else if (hit.transform.gameObject.layer == 9)  // 팔다리 레이어를 맞췄다면?
                {
                    hit.transform.gameObject.GetComponentInParent<PlayerTakeDamage>().TakeDamage(fireDamage);
                    ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                    Debug.Log("팔다리");
                }
                else if (hit.transform.gameObject.layer == 8)  // 헤드 레이어를 맞췄다면?
                {
                    hit.transform.gameObject.GetComponentInParent<PlayerTakeDamage>().TakeDamage(fireDamage * 2);
                    ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                    Debug.Log("헤드");
                }
                else
                {
                    // 오브젝트 풀로 hit 파티클 생성   
                    GameObject hitEffect = GameManager.Pool.Get(hitParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                    StartCoroutine(ReleaseRoutine(hitEffect));
                }

                // 트레일 생성 -> 트레일 이상해서 잠시 뺐음..
                StartCoroutine(TrailRoutine(muzzlePos.position, hit.point));
                ReleaseRoutine(trailEffect.gameObject);

                Rebound();
                OnFire?.Invoke();
            }
            else
            {
                // 트레일 생성
                StartCoroutine(TrailRoutine(muzzlePos.position, hit.point));
                ReleaseRoutine(trailEffect.gameObject);

                Rebound();
                OnFire?.Invoke();
            }
            anim.SetTrigger("Fire");
        }

        public override void Reload()    // 재장전
        {
            // 재장전은 Gun에서 할까?

            anim.SetTrigger("Reload");
            allBullet = allBullet - (availableBullet - curAvailavleBullet);
            curAvailavleBullet = availableBullet;
        }

        void OnReload(InputValue value)
        {
            Reload();
        }

        IEnumerator ReleaseRoutine(GameObject effect)   // 오브젝트 풀 Release 하기
        {
            yield return new WaitForSeconds(2f);
            GameManager.Pool.Release(effect);
        }

        IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
        {
            TrailRenderer trailrenderer = GameManager.Pool.Get(trailEffect, startPoint, Quaternion.identity);
            trailrenderer.Clear();

            // 트레일 루틴
            float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

            float rate = 0;
            while (rate < 1)
            {
                trailrenderer.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
                rate += Time.deltaTime / totalTime;

                // 시간에 따라 그 위치로 쭉 가도록
                yield return null;
            }
            GameManager.Pool.Release(trailrenderer.gameObject);
        }

        void Rebound()      // 반동
        {
            // 카메라 위로 살짝 움직이기
            camera.m_Tilt = Mathf.Lerp(camera.m_Tilt, camera.m_Tilt - 0.8f, 0.5f);
        }

    }
}