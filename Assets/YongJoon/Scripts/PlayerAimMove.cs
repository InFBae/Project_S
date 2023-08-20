using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Photon.Pun;

public class PlayerAimMove : MonoBehaviourPunCallbacks
{
    [SerializeField] Camera cameraRoot;
    [SerializeField] Transform realAimTarget;
    [SerializeField] Transform playerAimTarget;
    [SerializeField] Transform shootRoot;
    [SerializeField] Transform fireRoot;
    [SerializeField] Transform zoomRoot;
    [SerializeField] float cameraSensitivity;
    [SerializeField] float lookDistance;

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;
    private bool isSit = false;
    Vector3 upTrans;
    Vector3 point;
    Vector3 v;
    Vector3 v2;
    private float sitValue = 0.7f;
    PlayerMover mover;
    PhotonView PV;


    private void OnEnable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    //private void OnDisable()
    //{
    //    UnityEngine.Cursor.lockState = CursorLockMode.None;
    //}

    private void Awake()
    {
        upTrans = new Vector3(0, 1.7f, 0);
        mover = GetComponent<PlayerMover>();
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        Rotate();
    }
    private void LateUpdate()
    {
        Look();
    }
    private void Rotate()
    {
        //// 보고있는 물체의 위치
        //Vector3 lookPoint = Camera.main.transform.position + Camera.main.transform.forward * lookDistance;
        //realAimTarget.position = lookPoint;
        ////new Vector3(playerAimTarget.position.x, lookPoint.y, playerAimTarget.position.z);
        //playerAimTarget.position = realAimTarget.position;
        

        if (PV.IsMine)
        {
            realAimTarget.position = cameraRoot.transform.position + cameraRoot.transform.forward * lookDistance;
            playerAimTarget.position = realAimTarget.position;
            point = realAimTarget.position;
            // 보고있는 방향의 수직값은 현재의 수직값 
            point.y = transform.position.y;
            // 룩포인트 보기
            transform.LookAt(point);
            //PV.RPC("LookRotateRPC", RpcTarget.MasterClient);
        }
        else
        {
            return;
        }
    }

    [PunRPC]
    public void LookRotateRPC()
    {
        transform.LookAt(point);
    }


    private void Look()
    {
        if (PV.IsMine)
        {
            // 수평
            yRotation += lookDelta.x * cameraSensitivity * Time.deltaTime;
            // 수직값
            xRotation -= lookDelta.y * cameraSensitivity * Time.deltaTime;

            shootRoot.transform.rotation = Quaternion.Euler(xRotation + 6f, yRotation + 6f, 0);

            // 최대치
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            // 카메라 루트를 회전하는식으로
            cameraRoot.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            upTrans.y = isSit ? 1f : 1.7f;
            cameraRoot.transform.position = transform.position + upTrans;

            //shootRoot.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

            v = shootRoot.forward.normalized * Mathf.Cos(-xRotation / 80);
            if (isSit)
            { 
                v.y -= sitValue;
                zoomRoot.position = new Vector3(zoomRoot.position.x, 1, zoomRoot.position.z);

            }//v.z = Mathf.Clamp(v.z - 0.2f, 0, 1);
            fireRoot.transform.position = v + shootRoot.position;/*transform.position+ upTrans + new Vector3(0, -xRotation/80, 0);*/
        }
        else
        {
            return;
        }

    }

    private void OnAim(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
    private void OnSit(InputValue value)
    {
        if (value.isPressed && mover.IsGrounded())
        {
            isSit = true;
        }
        else
        {
            isSit = false;
        }
    }
}
