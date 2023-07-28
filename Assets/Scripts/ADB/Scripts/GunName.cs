using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ahndabi
{
    public class GunName : Gun
    {
        [SerializeField] float maxDistance;     // �ִ� ��Ÿ�. 60
        [SerializeField] float damage;          // ������
        [SerializeField] GameObject hitParticle;
        [SerializeField] TrailRenderer trailRenderer;
        [SerializeField] float bulletSpeed;

        private void Update()
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxDistance, Color.green);
            // ForwardDirection();     // ���� ī�޶� ��� ��ġ�� �ٶ󺸵���
        }

        public override void Fire()
        {
            // ����ĳ��Ʈ�� ���� ��
            RaycastHit hit;

            // ī�޶� ��ġ���� ī�޶� ��(���)�� ����ĳ��Ʈ�� maxDistance��ŭ ���µ� �ε��� ��ü�� �ִٸ�
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
            {
                // ������Ʈ Ǯ�� hit ��ƼŬ ����   
                GameObject hitEffect = GameManager.Pool.Get(hitParticle);
                hitEffect.transform.position = hit.point;
                hitEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
                hitEffect.transform.parent = hit.transform;
                StartCoroutine(ReleaseRoutine(hitEffect));

                // Ʈ���� ����
                TrailRenderer trail = Instantiate(trailRenderer, muzzlePos.transform.position, Quaternion.identity);
                StartCoroutine(TrailRoutine(trail, muzzlePos.position, hit.point));

                //GameManager.Pool.Release(hitEffect);
                OnFire?.Invoke();
                Debug.Log("Fire");
            }

        }

        IEnumerator ReleaseRoutine(GameObject effect)   // ������Ʈ Ǯ Release �ϱ�
        {
            yield return new WaitForSeconds(2f);
            GameManager.Pool.Release(effect);
        }

        IEnumerator TrailRoutine(TrailRenderer trail, Vector3 startPoint, Vector3 endPoint)
        {
            // Ʈ���� ��ƾ

            float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

            float rate = 0;
            while (rate < 1)
            {
                trail.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
                rate += Time.deltaTime / totalTime;

                // �ð��� ���� �� ��ġ�� �� ������
                yield return null;
            }
        }
    }
}