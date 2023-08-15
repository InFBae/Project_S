using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ahndabi;

namespace ahndabi
{
    public class ADB_RE_PlayerTakeDamage : ADB_RE_Player
    {
        private void Start()
        {
            // *** Debuging 모드 ***
            // StartCoroutine(DieDebug());
        }

        public void TakeDamage(int damage, Photon.Realtime.Player enemyPlayer)    // 데미지 받기
        {
            Debug.Log("TakeDamage");
            DecreaseHp(damage);
            StatusUI.OnHPChanged?.Invoke(Hp);
            anim.SetTrigger("TakeDamage");

            if (Hp <= 0)    // hp가 0이 되면 죽는다.
            {
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().killCount++;    // 상대의 킬 카운트 +1
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().ChangeKillCount();
                // 위 두 개는 밑 이벤트로 대체

                KillManager.OnKilled?.Invoke(enemyPlayer, this.player, false);      // 죽인사람, 죽은사람, 헤드샷 판정


                //StatusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);
                //KillDeathUI.OnKilled(gameObject, Player)
                deathCount++;   // 자신의 death 카운트 +1
                ChangeDeathCount();
                Die();
            }
        }

        public void TakeDamage(int damage, Photon.Realtime.Player enemyPlayer, bool headShot)    // 헤드샷 받은 데미지
        {
            Debug.Log("TakeDamage");
            DecreaseHp(damage);
            StatusUI.OnHPChanged?.Invoke(Hp);
            anim.SetTrigger("TakeDamage");

            if (Hp <= 0)    // hp가 0이 되면 죽는다.
            {
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().killCount++;    // 상대의 킬 카운트 +1
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().ChangeKillCount();
                // 위 두 개는 밑 이벤트로 대체

                KillManager.OnKilled?.Invoke(enemyPlayer, this.player, true);      // 죽인사람, 죽은사람, 헤드샷 판정

                // 헤드샷은 TakeDamage를 호출한 곳이 헤드를 맞춘 곳이라면 true

                //StatusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);
                //KillDeathUI.OnKilled(gameObject, Player)
                deathCount++;   // 자신의 death 카운트 +1
                ChangeDeathCount();
                Die();
            }
        }

        void Die()
        {
            diePlayer.SetActive(true);      // diePlayer 활성화(죽는 애니메이터를 따로 달아줘서 활성화 되자마자 알아서 Die 애니메이션 실행)
            gameObject.SetActive(false);    // 기존 Player는 비활성화
        }

        IEnumerator DieDebug()      // *** Debug 모드 ***
        {
            yield return new WaitForSeconds(5f);
            Die();
        }

        public void ChangeDeathCount()
        {
            //killDeathUI.ChagneKillDeathTextUI(killCount, deathCount);
        }
    }
}
