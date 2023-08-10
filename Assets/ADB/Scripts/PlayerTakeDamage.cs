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

            // *** Debuging ��� ***
            // StartCoroutine(DieDebug());
        }

        public void TakeDamage(int damage)    // ������ �ޱ�
        {
            Debug.Log("TakeDamage");
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
            diePlayer.SetActive(true);      // diePlayer Ȱ��ȭ(�״� �ִϸ����͸� ���� �޾��༭ Ȱ��ȭ ���ڸ��� �˾Ƽ� Die �ִϸ��̼� ����)
            gameObject.SetActive(false);    // ���� Player�� ��Ȱ��ȭ
        }
        
        IEnumerator DieDebug()      // *** Debug ��� ***
        {
            yield return new WaitForSeconds(5f);
            Die();
        }
    }
}
