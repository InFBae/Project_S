using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ahndabi
{
    public class PlayerAttacker : Player
    {
        void Fire()
        {
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("reloading"))
                return;

            gun.Fire();
            anim.SetTrigger("Fire");

            // gun.hit�� �޾ƿͼ�, ���� hit�� hp�� 0�� �Ǿ��ٸ� kil �� +1
            // gun.hit�� tag�� �÷��̾��̰� && GetComponent<Player>�� HP�� 0�̶��
            if (gun.hit.transform.tag == "Player" && gun.hit.transform.gameObject.GetComponent<Player>().Hp == 0)
            {
                killCount++;
            }
        }

        void OnFire(InputValue value)
        {
            Fire();
        }

        void OnReload(InputValue value)
        {
            gun.Reload();
            anim.SetTrigger("Reload");
        }
    }
}
