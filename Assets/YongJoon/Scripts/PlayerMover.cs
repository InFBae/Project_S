using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float nomalSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Camera cam;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 moveDir;
    private Vector3 moveVec;
    private Vector3 dir;
    private float curSpeed;
    private float zSpeed = 0; // 위, 아래
    private bool isWalk = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        // 부딪혔을 떄 회전 방지
        rb.freezeRotation = true;
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 떠있을때 벽에 매달리기 방지
        if (!IsGrounded())
        {
            return;
        }
        if (moveDir.magnitude == 0)
        {
            curSpeed = 0;
        }
        else
        {
            dir = cam.transform.localRotation * moveDir;
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
            }
            yield return null;
        }
    }
    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * 1f,
            0.5f, Vector3.down, out hit, 0.6f);
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
}
