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
        [SerializeField] float maxDistance;     // �ִ� ��Ÿ�. 60
        [SerializeField] GameObject hitParticle;
        [SerializeField] TrailRenderer trailEffect;
        [SerializeField] CinemachineRecomposer camera;
        [SerializeField] float bulletSpeed;
        [SerializeField] float fireCoolTime;        // ���� ������ ��Ÿ��
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
            ContinueousFire();      // ����

            // ForwardDirection();     // ���� ī�޶� ��� ��ġ�� �ٶ󺸵���
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        void ContinueousFire()      // ����
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
            // ����ĳ��Ʈ�� ���� ��
            RaycastHit hit;

            // ī�޶� ��ġ���� ī�޶� ��(���)�� ����ĳ��Ʈ�� maxDistance��ŭ ���µ� �ε��� ��ü�� �ִٸ�
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
            {
                // TODO : �ٵ�/��弦 ������ layer�� ��. �ݶ��̴� ���� ���� �� layer�� ���̰�(�ٵ�layer, ���layer) �� layer�� �����Ǹ� ������ ��

                if (hit.transform.parent.CompareTag("Player"))
                {
                    if (hit.transform.name == "mixamorig:Head")
                    {
                        // ������ playerTakeDamage(fireDamage)�� ȣ���ؾ��� -> hit.transform.gameObject.playerTakeDamage(fireDamage) �̷� ������!
                        hit.transform.gameObject.GetComponentInChildren<PlayerTakeDamage>().TakeDamage(fireDamage);
                        Debug.Log("��弦");
                        // �� ������ ��ƼŬ
                    }

                    Debug.Log("��� ����");
                    hit.transform.gameObject.GetComponentInParent<PlayerTakeDamage>().TakeDamage(fireDamage);

                    // hit ��ƼŬ ���� ��� ������ �� ������ ��ƼŬ�� �ٲ��ָ� �ɵ�
                }
                else
                {
                    // ������Ʈ Ǯ�� hit ��ƼŬ ����   
                    GameObject hitEffect = GameManager.Pool.Get(hitParticle);   // TODO : �̰� Ʈ����ó�� �ؿ��͵� �� �ٷ� �� ����
                    hitEffect.transform.position = hit.point;
                    hitEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
                    hitEffect.transform.parent = hit.transform;
                    StartCoroutine(ReleaseRoutine(hitEffect));
                }

                // Ʈ���� ���� -> Ʈ���� �̻��ؼ� ��� ����..
                // StartCoroutine(TrailRoutine(muzzlePos.position, hit.point));
                // ReleaseRoutine(trailEffect.gameObject);

                Rebound();
                OnFire?.Invoke();
            }
            else
            {
                // Ʈ���� ����
               // StartCoroutine(TrailRoutine(muzzlePos.position, hit.point));
               // ReleaseRoutine(trailEffect.gameObject);
                Rebound();
                OnFire?.Invoke();
            }
        }

        IEnumerator ReleaseRoutine(GameObject effect)   // ������Ʈ Ǯ Release �ϱ�
        {
            yield return new WaitForSeconds(2f);
            GameManager.Pool.Release(effect);
        }

        IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
        {
            // ������Ʈ Ǯ�� Ʈ���Ϸ����� ���� TODO : Ʈ���� �������� ������Ʈ Ǯ�� �ȵȴ�..
            TrailRenderer trailrenderer = GameManager.Pool.Get(trailEffect, startPoint, Quaternion.identity);
            trailrenderer.Clear();

            // Ʈ���� ��ƾ
            float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

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
            camera.m_Tilt = Mathf.Lerp(camera.m_Tilt, camera.m_Tilt - 0.8f, 0.5f);
        }
    }
}