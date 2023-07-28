using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ahndabi
{
    public class GunName : Gun
    {
        [SerializeField] float maxDistance;     // �ִ� ��Ÿ�. 60
        [SerializeField] float damage;          // ������
        [SerializeField] ParticleSystem hitParticle;

        private void Update()
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxDistance, Color.green);
            ForwardDirection();     // ���� ī�޶� ��� ��ġ�� �ٶ󺸵���
        }

        public override void Fire()
        {
            // ����ĳ��Ʈ�� ���� ��
            RaycastHit hit;

            // ī�޶� ��ġ���� ī�޶� ��(���)�� ����ĳ��Ʈ�� maxDistance��ŭ ���µ� �ε��� ��ü�� �ִٸ�
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