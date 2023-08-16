using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using JBB;

public class RE_PlayerTakeDamage : RE_Player
{
    [SerializeField] public PhotonView PV;
    public void TakeDamageRequest(int damage, Photon.Realtime.Player enemyPlayer, Photon.Realtime.Player damagedPlayer, bool headShot = false)    // 헤드샷 받은 데미지
    {
        PV.RPC("TakeDamage", RpcTarget.All, damage, enemyPlayer, damagedPlayer, headShot);
    }
    /*
    [PunRPC]
    public void TakeDamage(int damage, Photon.Realtime.Player enemyPlayer, Photon.Realtime.Player damagedPlayer ,bool headShot)
    {
        if (damagedPlayer.IsLocal)
        {
            Debug.Log($"{damagedPlayer.GetNickname()} TakeDamage by {enemyPlayer.GetNickname()}");
            DecreaseHp(damage);
            anim.SetTrigger("TakeDamage");

            if (Hp <= 0)    // hp가 0이 되면 죽는다.
            {
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().killCount++;    // 상대의 킬 카운트 +1
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().ChangeKillCount();
                // 위 두 개는 밑 이벤트로 대체

                JBB.GameSceneManager.OnKilled?.Invoke(enemyPlayer, damagedPlayer, headShot);      // 죽인사람, 죽은사람, 헤드샷 판정

                Die();
            }
        }

    }
    */
    public void Die()
    {
        // 충돌체 등의 문제 때문에 죽는 애니메이션을 하는 플레이어를 하나 따로 두어서
        // 이 함수가 호출되면 기존의 플레이어는 비활성화, DIe플레이어는 활성화시키고 애니메이션 발동한뒤에
        // 애니메이션 끝나면 플레이어 Destroy
        // diePlayer는 기존플레이어의 transform을 계속 따라다녀야 함

        diePlayer.SetActive(true);      // diePlayer 활성화(죽는 애니메이터를 따로 달아줘서 활성화 되자마자 알아서 Die 애니메이션 실행)
        gameObject.SetActive(false);    // 기존 Player는 비활성화
    }

}
