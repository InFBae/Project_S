using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FPSPlayerAnim : MonoBehaviour
{
    [SerializeField] RE_GunName gun;
    [SerializeField] Transform camTrans;
    [SerializeField] Transform ZoomTrans;
    


    private Animator anim;
    private Vector3 defaltCamTrans;
    private bool isFire = false;
    private bool isReload = false;
    private bool isZoom = false;
    Coroutine reloadRoutine;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        defaltCamTrans = camTrans.position;
    }
    private void Start()
    {
        StartCoroutine(FireAnimRoutine());
    }
    private void Update()
    {

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
        if(isReload)
        {
            return;
        }
        else
        {
            isReload = true;
            reloadRoutine = StartCoroutine(ReloadRoutine());
        }
    }

    IEnumerator ReloadRoutine()
    {
        bool tempIsReload = isReload;
        while (true)
        {
            if (tempIsReload != isReload)
            {
                isReload = tempIsReload;
                StopReload();
                yield return null;
            }
            else
            {
                anim.SetTrigger("Reload");
                tempIsReload = false;
                yield return new WaitForSeconds(3.08f);
            }
        }
    }
    private void StopReload()
    {
        StopCoroutine(reloadRoutine);
    }
    private void OnZoom(InputValue value)
    {
        if (value.isPressed)
        {
            if (isZoom)
            {
                return;
            }
            else
            {
                isZoom = true;
                camTrans.position = ZoomTrans.position;
                anim.SetBool("IsZoom", true);
            }
        }
        else
        {
            isZoom = false;
            camTrans.position = defaltCamTrans;
            anim.SetBool("IsZoom", false);
        }
    }

}
