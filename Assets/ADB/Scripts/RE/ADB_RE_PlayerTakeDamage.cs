using ahndabi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ADB_RE_PlayerTakeDamage : ADB_RE_Player
{
    [SerializeField] StatusUI statusUI;

    private void Start()
    {
        statusUI.HpTextUI.text = Hp.ToString();
        // *** Debuging ��� ***
        // StartCoroutine(DieDebug());
    }

    public void TakeDamage(int damage, GameObject enemyPlayer)    // ������ �ޱ�
    {
        Debug.Log("TakeDamage");
        DecreaseHp(damage);
        statusUI.DecreaseHPUI(damage);
        anim.SetTrigger("TakeDamage");

        if (Hp <= 0)    // hp�� 0�� �Ǹ� �״´�.
        {
            enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().killCount++;    // ����� ų ī��Ʈ +1
            enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().ChangeKillCount();
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
        killDeathUI.ChagneKillDeathTextUI(killCount, deathCount);
    }
}