using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] GameObject playerBody;
    [SerializeField] private float nomalSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Camera cam;
    [SerializeField] RE_GunName gun;
    
    private LayerMask layerMask = 64;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 moveDir;
    private Vector3 moveVec;
    private Vector3 dir;
    private float curSpeed;
    private float zSpeed = 0; // 위, 아래
    private bool isWalk = false;
    PhotonView PV;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        // 부딪혔을 떄 회전 방지
        rb.freezeRotation = true;
        PV = GetComponentInParent<PhotonView>();
    }
    private void Start()
    {
        //playerBody.SetActive(false);
        if (PV.IsMine)
        {
            playerBody.gameObject.layer = 4;
            ChangeLayerRecursively(playerBody, 4);
        }
    }
    private void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, layer);
        }
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 떠있을때 벽에 매달리기 방지
        if (!IsGrounded() && !IsClimbable())
        {
            //Debug.Log("is Float");
            return;
        }
        // 각도 계산해 특정 각도는 올라갈 수 있게
        if (IsClimbable())
        {
            //Debug.Log("isClimbable");
            rb.MovePosition(transform.position + Vector3.up * 0.3f);
        }
        if (moveDir.magnitude == 0)
        {
            rb.velocity = new Vector3(0, moveVec.y, 0);
            curSpeed = 0;
        }
        else
        {
            dir = transform.localRotation * moveDir;
            dir = new Vector3(dir.x, 0f, dir.z);

            curSpeed = isWalk ? walkSpeed : nomalSpeed;

            moveVec = dir * curSpeed;
            rb.velocity = moveVec + Vector3.up * rb.velocity.y;
        }
        anim.SetFloat("XSpeed", moveDir.x, 0.1f, Time.deltaTime);
        anim.SetFloat("YSpeed", moveDir.z, 0.1f, Time.deltaTime);
        anim.SetFloat("Speed", curSpeed);
    }
    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        StartCoroutine(JumpRoutine());
    }
    IEnumerator JumpRoutine()
    {
        while (true)
        {
            zSpeed += Physics.gravity.y * Time.deltaTime;
            if(IsGrounded() && zSpeed < 0)
            {
                zSpeed = -1;
                anim.SetTrigger("JumpEnd");
                StopAllCoroutines();
            }
            transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);
            //rb.velocity = new Vector3(moveVec.x, zSpeed, moveVec.z);
            yield return null;
        }
    }
    public bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * 1f,
            0.5f, Vector3.down, out hit, 0.6f, layerMask);
    }
    //private bool IsFloating()
    //{
    //    RaycastHit front;
    //    RaycastHit down;

    //    return (Physics.SphereCast(transform.position, 0.3f, Vector3.down, out down,  layerMask)) && Physics.SphereCast(transform.position + Vector3.up * 1.6f, 1f, transform.forward, out front, layerMask);
    //}
    private bool IsClimbable()
    {
        Ray upRay = new Ray(transform.position + Vector3.up * 0.01f, transform.forward);
        Ray downRay = new Ray(transform.position + Vector3.up * 0.001f, (Vector3.down + transform.forward).normalized);
        Ray realDownRay = new Ray(transform.position + Vector3.up * 0.01f, Vector3.down);

        RaycastHit upHit;
        RaycastHit downHit;
        RaycastHit realDownHit;

        if(Physics.Raycast(upRay, out upHit, 1.5f, layerMask, QueryTriggerInteraction.Ignore) &&
        Physics.Raycast(downRay, out downHit, 0.5f, layerMask, QueryTriggerInteraction.Ignore))
        { 
            Vector3 upVec = upHit.point;
            Vector3 downVec = downHit.point;

            Vector3 direction = upVec - downVec;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if(angle <80f && angle >-0.1f)
            {
                return true;
            }
            else { return false; }
        }
        else if(Physics.Raycast(upRay, out upHit, 1.5f, layerMask, QueryTriggerInteraction.Ignore) &&
            Physics.Raycast(realDownRay, out realDownHit, 0.5f, layerMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 upVec = upHit.point;
            Vector3 realDownVec = realDownHit.point;

            Vector3 direction = upVec - realDownVec;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 80 && angle > -80)
            {
                return true;
            }
            else { return false; }

        }
        else { return false; }
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);
    }
    private void OnWalk(InputValue value)
    {
        isWalk = value.isPressed ? true : false;
    }
    private void OnJump(InputValue value)
    {
        if (IsGrounded())
        {
            anim.SetTrigger("Jump");
            Jump();
        }
    }
    private void OnSit(InputValue value)
    {
        if (value.isPressed)
        {
            if (IsGrounded())
            {
                anim.SetBool("Crouch", true);
            }
        }
        else
        {
            anim.SetBool("Crouch", false);
        }
    }
    private void OnReload(InputValue value)
    {
        if (gun.isReload)
        {
            return;
        }
        anim.SetTrigger("Reload");
    }
}
