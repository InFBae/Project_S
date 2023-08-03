using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ahndabi
{
    public class PlayerTakeDamage : Player
    {
        public void TakeDamage(float damage)    // ������ �ޱ�
        {
            Debug.Log($"{damage}");

            DecreaseHP(damage);     // hp ����

            if (hp <= 0)    // hp�� 0�� �Ǹ� �״´�.
            {
                hp = 0;
                Die();
            }
        }

        void Die()
        {
            // �浹ü ���� ���� ������ �״� �ִϸ��̼��� �ϴ� �÷��̾ �ϳ� ���� �ξ
            // �� �Լ��� ȣ��Ǹ� ������ �÷��̾�� ��Ȱ��ȭ, DIe�÷��̾�� Ȱ��ȭ��Ű�� �ִϸ��̼� �ߵ��ѵڿ�
            // �ִϸ��̼� ������ �÷��̾� Destroy
            // diePlayer�� �����÷��̾��� transform�� ��� ����ٳ�� ��

            gameObject.SetActive(false);    // ���� Player�� ��Ȱ��ȭ
            diePlayer.SetActive(true);      // diePlayer Ȱ��ȭ(�״� �ִϸ����͸� ���� �޾��༭ Ȱ��ȭ ���ڸ��� �˾Ƽ� Die �ִϸ��̼� ����)
        }

        void DecreaseHP(float damage)
        {
            hp -= damage;
        }
    }
}
