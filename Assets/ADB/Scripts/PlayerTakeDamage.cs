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

            // *** Debuging 모드 ***
            // StartCoroutine(DieDebug());
        }

        public void TakeDamage(int damage)    // 데미지 받기
        {
            Debug.Log("TakeDamage");
            DecreaseHp(damage);
            statusUI.DecreaseHPUI(damage);
            anim.SetTrigger("TakeDamage");

            if (Hp <= 0)    // hp가 0이 되면 죽는다.
            {
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
    }
}
