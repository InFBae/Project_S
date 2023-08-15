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
            // *** Debuging ��� ***
            // StartCoroutine(DieDebug());
        }

        public void TakeDamage(int damage, Photon.Realtime.Player enemyPlayer)    // ������ �ޱ�
        {
            Debug.Log("TakeDamage");
            DecreaseHp(damage);
            StatusUI.OnHPChanged?.Invoke(Hp);
            anim.SetTrigger("TakeDamage");

            if (Hp <= 0)    // hp�� 0�� �Ǹ� �״´�.
            {
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().killCount++;    // ����� ų ī��Ʈ +1
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().ChangeKillCount();
                // �� �� ���� �� �̺�Ʈ�� ��ü

                KillManager.OnKilled?.Invoke(enemyPlayer, this.player, false);      // ���λ��, �������, ��弦 ����


                //StatusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);
                //KillDeathUI.OnKilled(gameObject, Player)
                deathCount++;   // �ڽ��� death ī��Ʈ +1
                ChangeDeathCount();
                Die();
            }
        }

        public void TakeDamage(int damage, Photon.Realtime.Player enemyPlayer, bool headShot)    // ��弦 ���� ������
        {
            Debug.Log("TakeDamage");
            DecreaseHp(damage);
            StatusUI.OnHPChanged?.Invoke(Hp);
            anim.SetTrigger("TakeDamage");

            if (Hp <= 0)    // hp�� 0�� �Ǹ� �״´�.
            {
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().killCount++;    // ����� ų ī��Ʈ +1
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().ChangeKillCount();
                // �� �� ���� �� �̺�Ʈ�� ��ü

                KillManager.OnKilled?.Invoke(enemyPlayer, this.player, true);      // ���λ��, �������, ��弦 ����

                // ��弦�� TakeDamage�� ȣ���� ���� ��带 ���� ���̶�� true

                //StatusUI.OnBulletCountChanged?.Invoke(curAvailavleBullet, remainBullet);
                //KillDeathUI.OnKilled(gameObject, Player)
                deathCount++;   // �ڽ��� death ī��Ʈ +1
                ChangeDeathCount();
                Die();
            }
        }

        void Die()
        {
            diePlayer.SetActive(true);      // diePlayer Ȱ��ȭ(�״� �ִϸ����͸� ���� �޾��༭ Ȱ��ȭ ���ڸ��� �˾Ƽ� Die �ִϸ��̼� ����)
            gameObject.SetActive(false);    // ���� Player�� ��Ȱ��ȭ
        }

        IEnumerator DieDebug()      // *** Debug ��� ***
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
