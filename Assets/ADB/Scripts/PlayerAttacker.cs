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

            // gun.hit을 받아와서, 지금 hit의 hp가 0이 되었다면 kil 수 +1
            // gun.hit의 tag가 플레이어이고 && GetComponent<Player>의 HP가 0이라면
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
