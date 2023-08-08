using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ahndabi
{
    public class PlayerTakeDamage : Player
    {
        [SerializeField] StatusUI statusUI;

        private void Start()
        {
            statusUI.HpTextUI.text = Hp.ToString();
        }

        public void TakeDamage(int damage)    // ������ �ޱ�
        {
            DecreaseHp(damage);
            statusUI.DecreaseHPUI(damage);
            anim.SetTrigger("TakeDamage");

            if (Hp <= 0)    // hp�� 0�� �Ǹ� �״´�.
            {
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
}
