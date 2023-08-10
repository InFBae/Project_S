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
        }

        void OnFire(InputValue value)
        {
            Fire();
        }

        void Reload(InputValue value)
        {
            gun.Reload();
            anim.SetTrigger("Reload");
        }
    }
}