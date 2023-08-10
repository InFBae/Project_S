using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ADB_RE_PlayerAttacker : RE_Player
{
    Coroutine fireRoutine;
    Coroutine fireStackRoutine;


    private bool isFire = false;



    private void Start()
    {
        fireStackRoutine = StartCoroutine(FireStackRoutine());
    }
    IEnumerator FireRoutine()
    {
        while (true)
        {
            if (isFire)
            {
                gun.Fire();
                yield return new WaitForSeconds(gun.FireCoolTime);
            }
            else
            {
                yield return null;
            }
        }
    }
    IEnumerator FireStackRoutine()
    {
        while (true)
        {
            if (isFire)
            {
                gun.fireStack++;
                yield return new WaitForSeconds(0.1f);

            }
            else if (gun.fireStack > 0)
            {
                gun.fireStack--;
                yield return new WaitForSeconds(0.05f);

            }
            yield return null;
        }
    }
    private void OnFire(InputValue value)
    {
        if (isFire == false)
        {
            fireRoutine = StartCoroutine(FireRoutine());
            Debug.Log(gun.boundValue);
            isFire = true;
        }
        else if(!value.isPressed)
        {
            StopCoroutine(fireRoutine);
            isFire = false;
        }
    }
    void OnReload(InputValue value)
    {
        gun.Reload();
        //anim.SetTrigger("Reload");
    }
}
