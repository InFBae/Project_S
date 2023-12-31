using ahndabi;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ADB_RE_GunName : ADB_RE_Gun
{
    [SerializeField] GameObject hitParticle;
    [SerializeField] ParticleSystem bloodParticle;
    [SerializeField] TrailRenderer trailEffect;
    [SerializeField] Camera cam;
    [SerializeField] float maxDistance;     // 최대 사거리. 60
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireCoolTime;        // 연발 나가는 쿨타임
    public float FireCoolTime { get { return fireCoolTime; } }
    public int GetCurBullet { get { return curAvailavleBullet; } }

    //float lastFireTime = 0f;
    float timer = 0f;
    //bool isMousePress = false;
    public bool isReload = false;
    public float boundValue = 0f;
    public int fireStack = 0;
    Coroutine reloadRoutine;



    private void Awake()
    {
        bloodParticle = GameManager.Resource.Load<ParticleSystem>("BloodParticle");
        hitParticle = GameManager.Resource.Load<GameObject>("HitEffect");
        trailEffect = GameManager.Resource.Load<TrailRenderer>("BulletTrail");
    }

    private void OnEnable()
    {
        remainBullet = 220;
        availableBullet = 100;
        fireDamage = 20;
        curAvailavleBullet = availableBullet;
        StatusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);
    }

    private void Update()
    {
        Debug.DrawRay(muzzlePos.transform.position, cam.transform.forward * maxDistance, Color.green);
        if (isReload)
        {
            fireStack = 0;
        }
        else if (boundValue > 0)
        {
            boundValue -= 0.01f;
            if (boundValue < 0) boundValue = 0f;
        }
        boundValue = Mathf.Clamp(0.02f * fireStack, 0f, 0.15f);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }



    //void ContinueousFire()      // 연발
    //{
    //    timer += Time.deltaTime;

    //    if (isFire)
    //    {
    //        if (timer >= fireCoolTime)
    //        {
    //            Fire();
    //            timer = 0f;
    //        }
    //    }
    //}



    public override void Fire()
    {
        if (curAvailavleBullet <= 0 || isReload/*anim.GetCurrentAnimatorStateInfo(0).IsName("reloading")*/)   // 총알 없으면 쏘지 못하도록
            return;

        --curAvailavleBullet;

        if (curAvailavleBullet < 0) curAvailavleBullet = 0;

        StatusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);
        //statusUI.DecreaseCurrentBulletUI(curAvailavleBullet);

        Vector3 camFwd = cam.transform.forward;

        Vector2 dir = new Vector2(Random.Range(-boundValue, boundValue), Random.Range(-boundValue, boundValue));
        Vector2 clampedDir = Vector2.ClampMagnitude(dir, boundValue);

        Vector3 rayShootDir = camFwd + Vector3.right * clampedDir.x * 2.5f + Vector3.up * clampedDir.y * 0.8f;
        //float radius = Random.Range(0, boundValue);
        //float angle = Random.Range(0, 10* Mathf.PI);
        //float maxValue = Mathf.(Mathf.Cos(angle));
        //Vector3 rayShootDir = new Vector3(camFwd.x + radius * Mathf.Cos(angle), camFwd.y + radius * Mathf.Sin(angle), camFwd.z * Mathf.Sin(angle));

        // 레이캐스트를 솼는데 부딪힌 물체가 있다면
        if (Physics.Raycast(muzzlePos.transform.position, rayShootDir /*cam.transform.forward + Vector3.right * 3f *Random.Range(-boundValue,boundValue)  + Vector3.up * Random.Range(-boundValue,boundValue)*/, out hit, maxDistance))
        {

            if (hit.transform.gameObject.layer == 7)  // 바디 레이어를 맞췄다면?
            {
                hit.transform.gameObject.GetComponentInParent<ADB_RE_PlayerTakeDamage>().TakeDamage(fireDamage, PhotonNetwork.LocalPlayer);
                ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
            }
            else if (hit.transform.gameObject.layer == 9)  // 팔다리 레이어를 맞췄다면?
            {
                hit.transform.gameObject.GetComponentInParent<ADB_RE_PlayerTakeDamage>().TakeDamage(fireDamage, PhotonNetwork.LocalPlayer);
                ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
            }
            else if (hit.transform.gameObject.layer == 8)  // 헤드 레이어를 맞췄다면?
            {
                hit.transform.gameObject.GetComponentInParent<ADB_RE_PlayerTakeDamage>().TakeDamage(fireDamage * 2, PhotonNetwork.LocalPlayer, true);
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

        }
        else
        {
            // 트레일 생성
            StartCoroutine(TrailRoutine(muzzlePos.position, hit.point));
            ReleaseRoutine(trailEffect.gameObject);
        }
    }

    public override void Reload()    // 재장전
    {
        if (remainBullet == 0)
            return;

        if ((remainBullet + curAvailavleBullet) <= availableBullet)
        {
            remainBullet = 0;
            curAvailavleBullet = remainBullet + curAvailavleBullet;
        }
        else
        {
            remainBullet = remainBullet - (availableBullet - curAvailavleBullet);
            curAvailavleBullet = availableBullet;
        }

        StatusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);

        if (isReload)
        {
            return;
        }
        else
        {
            isReload = true;
            reloadRoutine = StartCoroutine(ReloadRoutine());
        }
    }

    IEnumerator ReloadRoutine()
    {
        bool tempIsReload = isReload;
        while (true)
        {
            if(tempIsReload != isReload)
            {
                isReload = tempIsReload;
                StopReload();
                yield return null;
            }
            else
            {
                remainBullet = remainBullet - (availableBullet - curAvailavleBullet);
                curAvailavleBullet = availableBullet;
                tempIsReload = false;
                yield return new WaitForSeconds(3.08f);
            }
        }
    }
    private void StopReload()
    {
        StopCoroutine(reloadRoutine); 
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
        float totalTime = Vector3.Distance(startPoint, endPoint) / bulletSpeed;

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

}
