using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : Player
{
    // ���� damage��ŭ hp�� ���̰�, ���� 2�� damage��ŭ hp�� ����

    public void TakeDamage(float damage)
    {
        // �ݶ��̴��� �Ӹ��� �ϳ� ���� �� �Ӹ� �ݶ��̴��� ������ ������ 2��� �ϸ� �ȵǳ�
        // �ٵ� ĳ���Ϳ� �Ӹ��� ���� �ִµ� �� �Ӹ��� �±� �޾Ƽ� �� �±׸� ���߸� 2��� �ϸ�ȵǳ�


        // if (�Ӹ���)

        // else (���̸�)

    }

    void DecreaseHP(float damage)
    {
        hp -= damage;
    }
}
