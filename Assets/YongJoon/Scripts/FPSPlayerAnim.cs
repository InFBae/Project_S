using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FPSPlayerAnim : MonoBehaviour
{
    [SerializeField] RE_GunName gun;


    private Animator anim;
    private bool isFire = false;
    private bool isReload = false;
    Coroutine reloadRoutine;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(FireAnimRoutine());
    }
    private void Update()
    {
        if (!isReload)
        {
            StopCoroutine(reloadRoutine);
        }
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            isFire = true;
        }
        else
        {
            isFire = false;
        }
    }
    IEnumerator FireAnimRoutine()
    {
        while (true)
        {
            if (isFire && gun.IsBulletRemain() && !isReload)
            {
                anim.SetTrigger("Fire");
                yield return new WaitForSeconds(0.1f);
            }
            //else if(isFire && !gun.IsBulletRemain())
            //{
            //    anim.SetTrigger("Reload");
            //    yield return new WaitForSeconds(3.08f);
            //}
            yield return null;
        }
    }

    private void OnReload(InputValue value)
    {
        if(reloadRoutine != null)
        {
            isReload = false;
            return;
        }
        isReload = true;
        reloadRoutine = StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        while (true)
        {
            anim.SetTrigger("Reload");
            yield return new WaitForSeconds(3.08f);
            isReload = false;
            yield return null;
        }
    }
}
