using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ahndabi
{
    public class GunName : Gun
    {
        [SerializeField] float maxDistance;     // 최대 사거리. 60
        [SerializeField] float damage;          // 데미지
        [SerializeField] GameObject hitParticle;
        [SerializeField] TrailRenderer trailRenderer;
        [SerializeField] float bulletSpeed;

        private void Update()
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxDistance, Color.green);
            // ForwardDirection();     // 총이 카메라 가운데 위치를 바라보도록
        }

        public override void Fire()
        {
            // 레이캐스트로 총을 쏨
            RaycastHit hit;

            // 카메라 위치에서 카메라 앞(가운데)에 레이캐스트를 maxDistance만큼 쐈는데 부딪힌 물체가 있다면
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
            {
                // 오브젝트 풀로 hit 파티클 생성   
                GameObject hitEffect = GameManager.Pool.Get(hitParticle);
                hitEffect.transform.position = hit.point;
                hitEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
                hitEffect.transform.parent = hit.transform;
                StartCoroutine(ReleaseRoutine(hitEffect));

                // 트레일 생성
                TrailRenderer trail = Instantiate(trailRenderer, muzzlePos.transform.position, Quaternion.identity);
                StartCoroutine(TrailRoutine(trail, muzzlePos.position, hit.point));

                //GameManager.Pool.Release(hitEffect);
                OnFire?.Invoke();
                Debug.Log("Fire");
            }

        }

        IEnumerator ReleaseRoutine(GameObject effect)   // 오브젝트 풀 Release 하기
        {
            yield return new WaitForSeconds(2f);
            GameManager.Pool.Release(effect);
        }

        IEnumerator TrailRoutine(TrailRenderer trail, Vector3 startPoint, Vector3 endPoint)
        {
            // 트레일 루틴

            float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

            float rate = 0;
            while (rate < 1)
            {
                trail.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
                rate += Time.deltaTime / totalTime;

                // 시간에 따라 그 위치로 쭉 가도록
                yield return null;
            }
        }
    }
}