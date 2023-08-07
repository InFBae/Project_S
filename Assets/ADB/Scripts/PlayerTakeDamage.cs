using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ahndabi
{
    public class PlayerTakeDamage : Player
    {
        public void TakeDamage(float damage)    // 데미지 받기
        {
            DecreaseHP(damage);     // hp 감소
            anim.SetTrigger("TakeDamage");

            if (hp <= 0)    // hp가 0이 되면 죽는다.
            {
                hp = 0;
                Die();
            }
        }

        void Die()
        {
            // 충돌체 등의 문제 때문에 죽는 애니메이션을 하는 플레이어를 하나 따로 두어서
            // 이 함수가 호출되면 기존의 플레이어는 비활성화, DIe플레이어는 활성화시키고 애니메이션 발동한뒤에
            // 애니메이션 끝나면 플레이어 Destroy
            // diePlayer는 기존플레이어의 transform을 계속 따라다녀야 함

            diePlayer.SetActive(true);      // diePlayer 활성화(죽는 애니메이터를 따로 달아줘서 활성화 되자마자 알아서 Die 애니메이션 실행)
            gameObject.SetActive(false);    // 기존 Player는 비활성화
        }

        void DecreaseHP(float damage)
        {
            hp -= damage;
        }
    }
}
