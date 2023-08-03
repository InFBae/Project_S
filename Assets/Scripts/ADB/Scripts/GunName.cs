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
        [SerializeField] float maxDistance;     // 최대 사거리. 60
        [SerializeField] GameObject hitParticle;
        [SerializeField] TrailRenderer trailEffect;
        [SerializeField] CinemachineRecomposer camera;
        [SerializeField] float bulletSpeed;
        [SerializeField] float fireCoolTime;        // 연발 나가는 쿨타임
        float timer = 0f;
        bool isMousePress = false;
        PlayerTakeDamage playerTakeDamage;

        private void Start()
        {
            playerTakeDamage = GetComponentInParent<PlayerTakeDamage>();
            allBullet = 100;
            fireDamage = 30;
        }

        private void Update()
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxDistance, Color.green);
            ContinueousFire();      // 연발

            // ForwardDirection();     // 총이 카메라 가운데 위치를 바라보도록
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        void ContinueousFire()      // 연발
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMousePress = true;
            }

            if (isMousePress)
            {
                timer += Time.deltaTime;

                if(timer >= fireCoolTime)
                {
                    Fire();
                    timer = 0f;
                }
            }

            if (Input.GetMouseButtonUp(0)) 
            {
                isMousePress = false;
            }
        }

        public override void Fire()
        {
            // 레이캐스트로 총을 쏨
            RaycastHit hit;

            // 카메라 위치에서 카메라 앞(가운데)에 레이캐스트를 maxDistance만큼 쐈는데 부딪힌 물체가 있다면
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
            {
                // TODO : 바디/헤드샷 감지는 layer로 함. 콜라이더 붙인 곳에 다 layer를 붙이고(바디layer, 헤드layer) 그 layer가 감지되면 데미지 줌

                if (hit.transform.parent.CompareTag("Player"))
                {
                    if (hit.transform.name == "mixamorig:Head")
                    {
                        // 상대방의 playerTakeDamage(fireDamage)를 호출해야함 -> hit.transform.gameObject.playerTakeDamage(fireDamage) 이런 식으로!
                        hit.transform.gameObject.GetComponentInChildren<PlayerTakeDamage>().TakeDamage(fireDamage);
                        Debug.Log("헤드샷");
                        // 피 나오는 파티클
                    }

                    Debug.Log("사람 맞춤");
                    hit.transform.gameObject.GetComponentInParent<PlayerTakeDamage>().TakeDamage(fireDamage);

                    // hit 파티클 말고 사람 때리면 피 나오는 파티클로 바꿔주면 될듯
                }
                else
                {
                    // 오브젝트 풀로 hit 파티클 생성   
                    GameObject hitEffect = GameManager.Pool.Get(hitParticle);   // TODO : 이거 트레일처럼 밑에것들 한 줄로 다 적기
                    hitEffect.transform.position = hit.point;
                    hitEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
                    hitEffect.transform.parent = hit.transform;
                    StartCoroutine(ReleaseRoutine(hitEffect));
                }

                // 트레일 생성 -> 트레일 이상해서 잠시 뺐음..
                // StartCoroutine(TrailRoutine(muzzlePos.position, hit.point));
                // ReleaseRoutine(trailEffect.gameObject);

                Rebound();
                OnFire?.Invoke();
            }
            else
            {
                // 트레일 생성
               // StartCoroutine(TrailRoutine(muzzlePos.position, hit.point));
               // ReleaseRoutine(trailEffect.gameObject);
                Rebound();
                OnFire?.Invoke();
            }
        }

        IEnumerator ReleaseRoutine(GameObject effect)   // 오브젝트 풀 Release 하기
        {
            yield return new WaitForSeconds(2f);
            GameManager.Pool.Release(effect);
        }

        IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
        {
            // 오브젝트 풀로 트레일렌더러 생성 TODO : 트레일 렌더러가 오브젝트 풀이 안된다..
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