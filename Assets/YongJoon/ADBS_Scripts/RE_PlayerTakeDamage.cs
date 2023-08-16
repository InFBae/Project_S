using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RE_PlayerTakeDamage : RE_Player
{
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void TakeDamage(int damage, Photon.Realtime.Player enemyPlayer, bool headShot = false)    // ��弦 ���� ������
    {
        PV.RPC("HPControl", RpcTarget.All, damage, enemyPlayer, headShot);
    }
    [PunRPC]
    public void HPControl(int damage, Photon.Realtime.Player enemyPlayer, bool headShot)
    {
        Debug.Log("TakeDamage");
        DecreaseHp(damage);
        anim.SetTrigger("TakeDamage");

        if (Hp <= 0)    // hp�� 0�� �Ǹ� �״´�.
        {
            //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().killCount++;    // ����� ų ī��Ʈ +1
            //enemyPlayer.GetComponentInParent<ADB_RE_PlayerAttacker>().ChangeKillCount();
            // �� �� ���� �� �̺�Ʈ�� ��ü

            JBB.GameSceneManager.OnKilled?.Invoke(enemyPlayer, this.player, headShot);      // ���λ��, �������, ��弦 ����

            Die();
        }
    }
    void Die()
    {
        // �浹ü ���� ���� ������ �״� �ִϸ��̼��� �ϴ� �÷��̾ �ϳ� ���� �ξ
        // �� �Լ��� ȣ��Ǹ� ������ �÷��̾�� ��Ȱ��ȭ, DIe�÷��̾�� Ȱ��ȭ��Ű�� �ִϸ��̼� �ߵ��ѵڿ�
        // �ִϸ��̼� ������ �÷��̾� Destroy
        // diePlayer�� �����÷��̾��� transform�� ��� ����ٳ�� ��

        diePlayer.SetActive(true);      // diePlayer Ȱ��ȭ(�״� �ִϸ����͸� ���� �޾��༭ Ȱ��ȭ ���ڸ��� �˾Ƽ� Die �ִϸ��̼� ����)
        gameObject.SetActive(false);    // ���� Player�� ��Ȱ��ȭ
    }

}
