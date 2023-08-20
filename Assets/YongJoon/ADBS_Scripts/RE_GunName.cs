using Cinemachine;
using Cinemachine.Utility;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class RE_GunName : RE_Gun
{
    [SerializeField] public Transform zoomRoot;
    [SerializeField] public GameObject hitParticle;
    //[SerializeField] GameObject prefabMaster;
    [SerializeField] public ParticleSystem bloodParticle;
    [SerializeField] public TrailRenderer trailEffect;
    [SerializeField] Camera cam;
    [SerializeField] public float maxDistance;     // 최대 사거리. 60
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float fireCoolTime;        // 연발 나가는 쿨타임
    [SerializeField] public AudioClip clip;
    [SerializeField] public JBB.StatusUI statusUI;

    public float FireCoolTime { get { return fireCoolTime; } }
    public int GetCurBullet { get { return curAvailavleBullet; } }

    //float lastFireTime = 0f;
    float timer = 0f;
    //bool isMousePress = false;
    public bool isReload = false;
    public float boundValue = 0f;
    public int fireStack = 0;
    public Coroutine reloadRoutine;
    public bool isZoom = false;
    [SerializeField] PhotonView PV;
    public Vector3 realFireRoot;

    private void Awake()
    {
        bloodParticle = GameManager.Resource.Load<ParticleSystem>("BloodParticle");
        hitParticle = GameManager.Resource.Load<GameObject>("HitEffect");
        trailEffect = GameManager.Resource.Load<TrailRenderer>("BulletTrail");
    }

    private void Start()
    {
        remainBullet = 120;
        availableBullet = 30;
        fireDamage = 20;
        curAvailavleBullet = availableBullet;
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

        if (Input.GetMouseButtonDown(1))
        {
            isZoom = true;
        }else if (Input.GetMouseButtonUp(1))
        {
            isZoom = false;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
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

    public void FireRequest()
    {
        if (curAvailavleBullet <= 0 || isReload/*anim.GetCurrentAnimatorStateInfo(0).IsName("reloading")*/)   // 총알 없으면 쏘지 못하도록
            return;

        --curAvailavleBullet;
        if (curAvailavleBullet < 0) curAvailavleBullet = 0;

        statusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);

        Vector3 camFwd = cam.transform.forward;

        Vector2 dir = new Vector2(Random.Range(-boundValue, boundValue), Random.Range(-boundValue, boundValue));
        Vector2 clampedDir = Vector2.ClampMagnitude(dir, boundValue);

        if (isZoom)
        {
            realFireRoot = zoomRoot.position;
            clampedDir = Vector3.zero;
        }
        else
        {
            realFireRoot = muzzlePos.transform.position;
        }

        Vector3 rayShootDir = camFwd + Vector3.right * clampedDir.x * 1.5f + Vector3.up * clampedDir.y * 0.8f;

        PV.RPC("Fire", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, realFireRoot, rayShootDir);
    }

    public override void Fire(Photon.Realtime.Player shooter, Vector3 realFireRoot, Vector3 rayShootDir)
    {
        RaycastHit hit;   
        
        Vector3 targetTransform;
        int layerMask = LayerMask.GetMask("Environment", "PlayerBody", "PlayerHead", "PlayerArmsAndLegs");
        // 레이캐스트를 굔쨉 부딪힌 물체가 있다면
        if (Physics.Raycast(realFireRoot, rayShootDir, out hit, maxDistance, layerMask/*768*/))
        {
            RE_PlayerTakeDamage damagedPlayer = hit.collider.gameObject.GetComponentInParent<RE_PlayerTakeDamage>();
            if (hit.collider.gameObject.layer == 7)  // 바디 레이어를 맞췄다면?
            {                
                damagedPlayer.TakeDamageRequest(fireDamage, shooter, damagedPlayer.PV.Owner);
                PV.RPC("MakeEffect", RpcTarget.All, "bloodParticle", hit.point, hit.normal);
                Debug.Log("바디");
            }
            else if (hit.collider.gameObject.layer == 9)  // 팔다리 레이어를 맞췄다면?
            {
                damagedPlayer.TakeDamageRequest(fireDamage, shooter, damagedPlayer.PV.Owner);
                PV.RPC("MakeEffect", RpcTarget.All, "bloodParticle", hit.point, hit.normal);
                Debug.Log("팔다리");
            }
            else if (hit.collider.gameObject.layer == 8)  // 헤드 레이어를 맞췄다면?
            {
                damagedPlayer.TakeDamageRequest(fireDamage, shooter, damagedPlayer.PV.Owner, true);
                PV.RPC("MakeEffect", RpcTarget.All, "bloodParticle", hit.point, hit.normal);
                Debug.Log("헤드");
            }
            else
            {
                // 오브젝트 풀로 hit 파티클 생성   
                PV.RPC("MakeEffect", RpcTarget.All ,"hitEffect", hit.point, hit.normal);
            }
            targetTransform = hit.point;
        }
        else
        {
            // 트레일 생성
            targetTransform = muzzlePos.forward * 200;
        }

        PV.RPC("MakeTrail", RpcTarget.All, realFireRoot, targetTransform);
        PV.RPC("FireSound", RpcTarget.All, realFireRoot);

        Debug.Log("Fire");
    }
    /*
    [PunRPC]
    public void FireSound(Vector3 muzzlePoint)
    {
        AudioSource.PlayClipAtPoint(clip, muzzlePoint);
    }

    [PunRPC]
    public void MakeTrail(Vector3 start, Vector3 end)
    {
        StartCoroutine(TrailRoutine(start, end));
    }

    [PunRPC]
    public void MakeEffect(string name, Vector3 hitPoint, Vector3 hitRotation)
    {
        if (name == "hitEffect")
        {
            GameObject hitEffect = GameManager.Pool.Get(hitParticle, hitPoint, Quaternion.LookRotation(hitRotation));
            StartCoroutine(ReleaseRoutine(hitEffect));
        }
        else if(name == "bloodParticle")
        {            
            GameManager.Pool.Get(bloodParticle, hitPoint, Quaternion.LookRotation(hitRotation));
        }
    }
    //[PunRPC]
    //public void FireTrailRPC(Vector3 hitPoint)
    //{
    //    // 트레일 생성 -> 트레일 이상해서 잠시 뺐음..
    //    StartCoroutine(TrailRoutine(realFireRoot, hitPoint));
    //    ReleaseRoutine(trailEffect.gameObject);
    //}
    */
    public void ReloadRequest()
    {
        if (remainBullet <= 0)
            return;

        if (isReload)
        {
            return;
        }
        else
        {
            isReload = true;

            if ((remainBullet + curAvailavleBullet) <= availableBullet)
            {
                curAvailavleBullet = remainBullet + curAvailavleBullet;
                remainBullet = 0;
            }
            else
            {
                remainBullet = remainBullet - (availableBullet - curAvailavleBullet);
                curAvailavleBullet = availableBullet;
            }
            statusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);
            reloadRoutine = StartCoroutine(ReloadRoutine());
        }
        //PV.RPC("Reload", RpcTarget.All);
    }
    
    [PunRPC]
    public override void Reload()    // 재장전
    {
        
    }
    
    public IEnumerator ReloadRoutine()
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
                tempIsReload = false;
                yield return new WaitForSeconds(3.08f);
            }
        }
    }
    public void StopReload()
    {
        StopCoroutine(reloadRoutine); 
    }

    public IEnumerator ReleaseRoutine(GameObject effect)   // 오브젝트 풀 Release 하기
    {
        yield return new WaitForSeconds(2f);
        GameManager.Pool.Release(effect);
    }

    public IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
    {
        TrailRenderer trailrenderer = GameManager.Pool.Get(trailEffect, startPoint, Quaternion.identity);
        trailrenderer.Clear();

        // 트레일 루틴
        float totalTime = Vector3.Distance(startPoint, endPoint) / bulletSpeed;

        float rate = 0;
        while (rate < 1)
        {
            trailrenderer.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
            rate += (Time.deltaTime / totalTime);

            // 시간에 따라 그 위치로 쭉 가도록
            yield return null;
        }
        GameManager.Pool.Release(trailrenderer.gameObject);
    }
    //private void OnZoom(InputValue value)
    //{
    //    if (value.isPressed)
    //    {
    //        if (isZoom)
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            isZoom = true;
    //        }
    //    }
    //    else
    //    {
    //        isZoom = false;
    //    }
    //}
}
