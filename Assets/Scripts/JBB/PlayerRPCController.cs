using ahndabi;
using JBB;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerRPCController : MonoBehaviourPun
{
    PhotonView PV;
    [SerializeField] RE_PlayerTakeDamage playerTakeDamage;
    [SerializeField] RE_GunName gun;
    [SerializeField] GameObject killLogContent;
    RigBuilder rb;
    PlayerInput pInput;
    RE_PlayerAttacker playerAttacker;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        rb = GetComponentInChildren<RigBuilder>();
        pInput = GetComponent<PlayerInput>();
        playerAttacker = GetComponent<RE_PlayerAttacker>();
    }

    [PunRPC]
    public void Fire(Photon.Realtime.Player shooter, Vector3 realFireRoot, Vector3 rayShootDir)
    {
        RaycastHit hit;

        Vector3 targetTransform;
        int layerMask = LayerMask.GetMask("Environment", "PlayerBody", "PlayerHead", "PlayerArmsAndLegs");
        // 레이캐스트를 굔쨉 부딪힌 물체가 있다면
        if (Physics.Raycast(realFireRoot, rayShootDir, out hit, gun.maxDistance, layerMask/*768*/))
        {
            RE_PlayerTakeDamage damagedPlayer = hit.collider.gameObject.GetComponentInParent<RE_PlayerTakeDamage>();
            if (hit.collider.gameObject.layer == 7)  // 바디 레이어를 맞췄다면?
            {
                damagedPlayer.TakeDamageRequest(gun.fireDamage, shooter, damagedPlayer.PV.Owner);
                PV.RPC("MakeEffect", RpcTarget.All, "bloodParticle", hit.point, hit.normal);
                Debug.Log("바디");
            }
            else if (hit.collider.gameObject.layer == 9)  // 팔다리 레이어를 맞췄다면?
            {
                damagedPlayer.TakeDamageRequest(gun.fireDamage, shooter, damagedPlayer.PV.Owner);
                PV.RPC("MakeEffect", RpcTarget.All, "bloodParticle", hit.point, hit.normal);
                Debug.Log("팔다리");
            }
            else if (hit.collider.gameObject.layer == 8)  // 헤드 레이어를 맞췄다면?
            {
                damagedPlayer.TakeDamageRequest(gun.fireDamage, shooter, damagedPlayer.PV.Owner, true);
                PV.RPC("MakeEffect", RpcTarget.All, "bloodParticle", hit.point, hit.normal);
                Debug.Log("헤드");
            }
            else
            {
                // 오브젝트 풀로 hit 파티클 생성   
                PV.RPC("MakeEffect", RpcTarget.All, "hitEffect", hit.point, hit.normal);
            }
            targetTransform = hit.point;
        }
        else
        {
            // 트레일 생성
            targetTransform = gun.muzzlePos.forward * 200;
        }

        PV.RPC("MakeTrail", RpcTarget.All, realFireRoot, targetTransform);
        PV.RPC("FireSound", RpcTarget.All, realFireRoot);

        Debug.Log("Fire");
    }

    [PunRPC]
    public void FireSound(Vector3 muzzlePoint)
    {
        AudioSource.PlayClipAtPoint(gun.clip, muzzlePoint);
    }

    [PunRPC]
    public void MakeTrail(Vector3 start, Vector3 end)
    {
        StartCoroutine(gun.TrailRoutine(start, end));
    }

    [PunRPC]
    public void MakeEffect(string name, Vector3 hitPoint, Vector3 hitRotation)
    {
        if (name == "hitEffect")
        {
            GameObject hitEffect = GameManager.Pool.Get(gun.hitParticle, hitPoint, Quaternion.LookRotation(hitRotation));
            StartCoroutine(gun.ReleaseRoutine(hitEffect));
        }
        else if (name == "bloodParticle")
        {
            GameManager.Pool.Get(gun.bloodParticle, hitPoint, Quaternion.LookRotation(hitRotation));
        }
    }

    [PunRPC]
    public void TakeDamage(int damage, Photon.Realtime.Player enemyPlayer, Photon.Realtime.Player damagedPlayer, bool headShot)
    {
        if (damagedPlayer.IsLocal)
        {
            if (playerTakeDamage.Hp <= 0)
                return;
            if (enemyPlayer == damagedPlayer)
                return;

            Debug.Log($"{damagedPlayer.GetNickname()} TakeDamage by {enemyPlayer.GetNickname()}");

            playerTakeDamage.DecreaseHp(damage);
            playerTakeDamage.anim.SetTrigger("TakeDamage");

            if (playerTakeDamage.Hp <= 0)    // hp가 0이 되면 죽는다.
            {
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().killCount++;    // 상대의 킬 카운트 +1
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().ChangeKillCount();
                // 위 두 개는 밑 이벤트로 대체

                JBB.GameSceneManager.OnKilled?.Invoke(enemyPlayer, damagedPlayer, headShot);      // 죽인사람, 죽은사람, 헤드샷 판정
                PV.RPC("CreateKillLog", RpcTarget.All ,headShot, damagedPlayer);

                rb.enabled = false;
                pInput.enabled = false;
                //playerAttacker.gun.OnDisable();
                playerTakeDamage.anim.SetTrigger("Die");

                StartCoroutine(DieRoutine());

                playerTakeDamage.ResetHp();
                playerAttacker.gun.ResetGun();
                playerAttacker.RespawnGun();

                //PhotonNetwork.Destroy(transform.parent.parent.gameObject);
                //PhotonNetwork.Instantiate("DiePlayer", transform.position, transform.rotation);
               
            }
        }
    }
    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(5f);
        rb.enabled = true;
        pInput.enabled = true;
        playerTakeDamage.anim.SetTrigger("DieEnd");
        transform.position = Vector3.zero;
    }

    IEnumerator DestroyRoutine(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(gameObject);
    }


    GameObject killLogContent;
    [PunRPC]
    public void CreateKillLog(bool isHeadShot, Photon.Realtime.Player killed)
    {
        if (chattingContent == null)
        {
            chattingContent = FindObjectOfType<InGameChattingUI>().GetComponent<InGameChattingUI>().content;
        }
        KillLogText killLogText = GameManager.Pool.GetUI(GameManager.Resource.Load<KillLogText>("UI/KillLogText"));
        killLogText.SetKillLogText(isHeadShot, killed);
        killLogText.transform.parent = killLogContent.transform;        
        GameManager.Pool.ReleaseUI(killLogText.gameObject, 5f);
    }


    GameObject chattingContent;
    [PunRPC]
    public void CreateMessage(string text)
    {
        if (chattingContent == null)
        {
            chattingContent = FindObjectOfType<InGameChattingUI>().GetComponent<InGameChattingUI>().content;
        }
        ChatTextUI chat = GameManager.Pool.GetUI(GameManager.Resource.Load<ChatTextUI>("UI/ChatText"));
        chat.SetText(text);
        chat.transform.SetParent(chattingContent.transform, false);
    }

}
