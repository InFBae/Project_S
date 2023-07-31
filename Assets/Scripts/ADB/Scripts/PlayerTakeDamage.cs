using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : Player
{
    // 몸은 damage만큼 hp가 깎이고, 헤드는 2배 damage만큼 hp가 깎임

    public void TakeDamage(float damage)
    {
        // 콜라이더를 머리에 하나 만들어서 그 머리 콜라이더를 맞으면 데미지 2배로 하면 안되나
        // 근데 캐릭터에 머리만 따로 있는데 이 머리에 태그 달아서 이 태그를 맞추면 2배로 하면안되나


        // if (머리면)

        // else (몸이면)

    }

    void DecreaseHP(float damage)
    {
        hp -= damage;
    }
}
