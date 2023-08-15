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
    [SerializeField] Transform zoomRoot;
    [SerializeField] GameObject hitParticle;
    //[SerializeField] GameObject prefabMaster;
    [SerializeField] ParticleSystem bloodParticle;
    [SerializeField] TrailRenderer trailEffect;
    [SerializeField] Camera cam;
    [SerializeField] float maxDistance;     // �ִ� ��Ÿ�. 60
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireCoolTime;        // ���� ������ ��Ÿ��
    [SerializeField] AudioClip clip;

    public float FireCoolTime { get { return fireCoolTime; } }
    public int GetCurBullet { get { return curAvailavleBullet; } }

    //float lastFireTime = 0f;
    float timer = 0f;
    //bool isMousePress = false;
    public bool isReload = false;
    public float boundValue = 0f;
    public int fireStack = 0;
    Coroutine reloadRoutine;
    public bool isZoom = false;
    PhotonView PV;
    Vector3 realFireRoot;

    private void Awake()
    {
        bloodParticle = GameManager.Resource.Load<ParticleSystem>("BloodParticle");
        hitParticle = GameManager.Resource.Load<GameObject>("HitEffect");
        trailEffect = GameManager.Resource.Load<TrailRenderer>("BulletTrail");
        PV = GetComponent<PhotonView>();
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



    //void ContinueousFire()      // ����
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
        PV.RPC("Fire", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    public override void Fire(Photon.Realtime.Player shooter)
    {
        RaycastHit hit;

        if (curAvailavleBullet <= 0 || isReload/*anim.GetCurrentAnimatorStateInfo(0).IsName("reloading")*/)   // �Ѿ� ������ ���� ���ϵ���
            return;

        --curAvailavleBullet;
        if (curAvailavleBullet < 0) curAvailavleBullet = 0;

        JBB.StatusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);

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
        //float radius = Random.Range(0, boundValue);
        //float angle = Random.Range(0, 10* Mathf.PI);
        //float maxValue = Mathf.(Mathf.Cos(angle));
        //Vector3 rayShootDir = new Vector3(camFwd.x + radius * Mathf.Cos(angle), camFwd.y + radius * Mathf.Sin(angle), camFwd.z * Mathf.Sin(angle));
        
        Vector3 targetTransform;
        // ����ĳ��Ʈ�� ���µ� �ε��� ��ü�� �ִٸ�
        if (Physics.Raycast(realFireRoot, rayShootDir /*cam.transform.forward + Vector3.right * 3f *Random.Range(-boundValue,boundValue)  + Vector3.up * Random.Range(-boundValue,boundValue)*/, out hit, maxDistance))
        {
            
            if (hit.transform.gameObject.layer == 7)  // �ٵ� ���̾ ����ٸ�?
            {
                hit.transform.gameObject.GetComponentInParent<ADB_RE_PlayerTakeDamage>().TakeDamage(fireDamage, shooter);
                ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                Debug.Log("�ٵ�");
            }
            else if (hit.transform.gameObject.layer == 9)  // �ȴٸ� ���̾ ����ٸ�?
            {
                hit.transform.gameObject.GetComponentInParent<ADB_RE_PlayerTakeDamage>().TakeDamage(fireDamage, shooter);
                ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                Debug.Log("�ȴٸ�");
            }
            else if (hit.transform.gameObject.layer == 8)  // ��� ���̾ ����ٸ�?
            {
                hit.transform.gameObject.GetComponentInParent<ADB_RE_PlayerTakeDamage>().TakeDamage(fireDamage, shooter, true);
                ParticleSystem hitEffect = GameManager.Pool.Get(bloodParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                Debug.Log("���");
            }
            else
            {
                // ������Ʈ Ǯ�� hit ��ƼŬ ����   
                GameObject hitEffect = GameManager.Pool.Get(hitParticle, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                StartCoroutine(ReleaseRoutine(hitEffect));
            }
            targetTransform = hit.point;
        }
        else
        {
            // Ʈ���� ����
            targetTransform = muzzlePos.forward * 200;
        }

        PV.RPC("MakeTrail", RpcTarget.All, realFireRoot, targetTransform);
        PV.RPC("FireSound", RpcTarget.All, muzzlePos.position);

        Debug.Log("Fire");
    }

    [PunRPC]
    public void FireSound(Vector3 muzzlPoint)
    {
        AudioSource.PlayClipAtPoint(clip, muzzlPoint);
    }

    [PunRPC]
    public void MakeTrail(Vector3 start, Vector3 end)
    {
        StartCoroutine(TrailRoutine(start, end));
    }

    [PunRPC]
    public void FireTrailRPC(Vector3 hitPoint)
    {
        // Ʈ���� ���� -> Ʈ���� �̻��ؼ� ��� ����..
        StartCoroutine(TrailRoutine(realFireRoot, hitPoint));
        ReleaseRoutine(trailEffect.gameObject);
    }

    public override void Reload()    // ������
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

        JBB.StatusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);

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
            rate += (Time.deltaTime / totalTime);

            // �ð��� ���� �� ��ġ�� �� ������
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
