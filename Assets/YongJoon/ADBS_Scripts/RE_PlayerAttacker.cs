using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class RE_PlayerAttacker : RE_Player
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

    void OnReload(InputValue value)
    {
        gun.Reload();
        anim.SetTrigger("Reload");
    }
}
