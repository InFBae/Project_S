using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using JBB;

public class RE_PlayerTakeDamage : RE_Player
{
    [SerializeField] public PhotonView PV;
    public void TakeDamageRequest(int damage, Photon.Realtime.Player enemyPlayer, Photon.Realtime.Player damagedPlayer, bool headShot = false)    // ��弦 ���� ������
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

            if (Hp <= 0)    // hp�� 0�� �Ǹ� �״´�.
            {
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().killCount++;    // ����� ų ī��Ʈ +1
                //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().ChangeKillCount();
                // �� �� ���� �� �̺�Ʈ�� ��ü

                JBB.GameSceneManager.OnKilled?.Invoke(enemyPlayer, damagedPlayer, headShot);      // ���λ��, �������, ��弦 ����

                Die();
            }
        }

    }
    */
    public void Die()
    {
        // �浹ü ���� ���� ������ �״� �ִϸ��̼��� �ϴ� �÷��̾ �ϳ� ���� �ξ
        // �� �Լ��� ȣ��Ǹ� ������ �÷��̾�� ��Ȱ��ȭ, DIe�÷��̾�� Ȱ��ȭ��Ű�� �ִϸ��̼� �ߵ��ѵڿ�
        // �ִϸ��̼� ������ �÷��̾� Destroy
        // diePlayer�� �����÷��̾��� transform�� ��� ����ٳ�� ��

        diePlayer.SetActive(true);      // diePlayer Ȱ��ȭ(�״� �ִϸ����͸� ���� �޾��༭ Ȱ��ȭ ���ڸ��� �˾Ƽ� Die �ִϸ��̼� ����)
        gameObject.SetActive(false);    // ���� Player�� ��Ȱ��ȭ
    }

}
