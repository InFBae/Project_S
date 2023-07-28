using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ahndabi
{
    public class GunName : Gun
    {
        [SerializeField] float maxDistance;     // 최대 사거리. 60
        [SerializeField] float damage;          // 데미지
        [SerializeField] ParticleSystem hitParticle;

        private void Update()
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxDistance, Color.green);
            ForwardDirection();     // 총이 카메라 가운데 위치를 바라보도록
        }

        public override void Fire()
        {
            // 레이캐스트로 총을 쏨
            RaycastHit hit;

            // 카메라 위치에서 카메라 앞(가운데)에 레이캐스트를 maxDistance만큼 쐈는데 부딪힌 물체가 있다면
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
            {
                //Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
                //Destroy(hitParticle, 3f);
                ParticleSystem hitEffect = GameManager.Pool.Get(hitParticle);
                GameManager.Pool.Release(hitEffect);
                OnFire?.Invoke();
                Debug.Log("Fire");
            }

        }

    }
}