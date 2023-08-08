using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Yong
{
    public class RE_GunName : RE_Gun
    {
        [SerializeField] GameObject hitParticle;
        [SerializeField] ParticleSystem bloodParticle;
        [SerializeField] TrailRenderer trailEffect;
        [SerializeField] Camera cam;
        [SerializeField] float maxDistance;     // �ִ� ��Ÿ�. 60
        [SerializeField] float bulletSpeed;
        [SerializeField] float fireCoolTime;        // ���� ������ ��Ÿ��
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
            availableBullet = 100;
            fireDamage = 20;
            curAvailavleBullet = availableBullet;
        }

        private void Update()
        {
            Debug.DrawRay(muzzlePos.transform.position, cam.transform.forward * maxDistance, Color.green);
            ContinueousFire();      // ����
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        void ContinueousFire()      // ����
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

            if (curAvailavleBullet == 0)   // �Ѿ� ������ ���� ���ϵ���
                return;

            --curAvailavleBullet;

            // ����ĳ��Ʈ�� ���µ� �ε��� ��ü�� �ִٸ�
            if (Physics.Raycast(muzzlePos.transform.position, cam.transform.forward, out hit, maxDistance))
            {
                if (hit.transform.gameObject.layer == 7)  // �ٵ� ���̾ ����ٸ�?
                {
                    hit.transform.gameObject.GetComponentInParent<RE_PlayerTakeDamage>().TakeDamage(fireDamage);
                    ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                    Debug.Log("�ٵ�");
                }
                else if (hit.transform.gameObject.layer == 9)  // �ȴٸ� ���̾ ����ٸ�?
                {
                    hit.transform.gameObject.GetComponentInParent<RE_PlayerTakeDamage>().TakeDamage(fireDamage);
                    ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                    Debug.Log("�ȴٸ�");
                }
                else if (hit.transform.gameObject.layer == 8)  // ��� ���̾ ����ٸ�?
                {
                    hit.transform.gameObject.GetComponentInParent<RE_PlayerTakeDamage>().TakeDamage(fireDamage * 2);
                    ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                    Debug.Log("���");
                }
                else
                {
                    // ������Ʈ Ǯ�� hit ��ƼŬ ����   
                    GameObject hitEffect = GameManager.Pool.Get(hitParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                    StartCoroutine(ReleaseRoutine(hitEffect));
                }

                // Ʈ���� ���� -> Ʈ���� �̻��ؼ� ��� ����..
                StartCoroutine(TrailRoutine(muzzlePos.position, hit.point));
                ReleaseRoutine(trailEffect.gameObject);

                Rebound();
            }
            else
            {
                // Ʈ���� ����
                StartCoroutine(TrailRoutine(muzzlePos.position, hit.point));
                ReleaseRoutine(trailEffect.gameObject);

                Rebound();
            }
            Debug.Log("Fire");
        }

        public override void Reload()    // ������
        {
            allBullet = allBullet - (availableBullet - curAvailavleBullet);
            curAvailavleBullet = availableBullet;
        }

        IEnumerator ReleaseRoutine(GameObject effect)   // ������Ʈ Ǯ Release �ϱ�
        {
            yield return new WaitForSeconds(2f);
            GameManager.Pool.Release(effect);
        }

        IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
        {
            TrailRenderer trailrenderer = GameManager.Pool.Get(trailEffect, startPoint, Quaternion.identity);
            trailrenderer.Clear();

            // Ʈ���� ��ƾ
            float totalTime = Vector3.Distance(startPoint, endPoint) / bulletSpeed;

            float rate = 0;
            while (rate < 1)
            {
                trailrenderer.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
                rate += Time.deltaTime / totalTime;

                // �ð��� ���� �� ��ġ�� �� ������
                yield return null;
            }
            GameManager.Pool.Release(trailrenderer.gameObject);
        }

        void Rebound()      // �ݵ�
        {
            // ī�޶� ���� ��¦ �����̱�
            //camera.m_Tilt = Mathf.Lerp(camera.m_Tilt, camera.m_Tilt - 0.8f, 0.5f);
        }

    }
}