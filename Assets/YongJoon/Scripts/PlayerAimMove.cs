using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerAimMove : MonoBehaviour
{
    [SerializeField] Camera cameraRoot;
    [SerializeField] Transform realAimTarget;
    [SerializeField] Transform playerAimTarget;
    [SerializeField] Transform shootRoot;
    [SerializeField] Transform fireRoot;
    [SerializeField] float cameraSensitivity;
    [SerializeField] float lookDistance;

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;
    Vector3 upTrans;
    Vector3 point;
    Vector3 v;


    private void OnEnable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    private void Awake()
    {
        upTrans = new Vector3(0, 1.7f, 0);
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
        realAimTarget.position = cameraRoot.transform.position + cameraRoot.transform.forward * lookDistance;
        playerAimTarget.position = realAimTarget.position;
        point = realAimTarget.position;
        // 보고있는 방향의 수직값은 현재의 수직값 
        point.y = transform.position.y;
        // 룩포인트 보기
        transform.LookAt(point);
    }
    private void Look()
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
        cameraRoot.transform.position = transform.position + upTrans;

        //shootRoot.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        v = shootRoot.forward.normalized * Mathf.Cos(-xRotation/80);

        fireRoot.transform.position = v + shootRoot.position;/*transform.position+ upTrans + new Vector3(0, -xRotation/80, 0);*/

    }

    private void OnAim(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
