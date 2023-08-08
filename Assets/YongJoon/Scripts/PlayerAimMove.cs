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
        //// �����ִ� ��ü�� ��ġ
        //Vector3 lookPoint = Camera.main.transform.position + Camera.main.transform.forward * lookDistance;
        //realAimTarget.position = lookPoint;
        ////new Vector3(playerAimTarget.position.x, lookPoint.y, playerAimTarget.position.z);
        //playerAimTarget.position = realAimTarget.position;
        realAimTarget.position = cameraRoot.transform.position + cameraRoot.transform.forward * lookDistance;
        playerAimTarget.position = realAimTarget.position;
        point = realAimTarget.position;
        // �����ִ� ������ �������� ������ ������ 
        point.y = transform.position.y;
        // ������Ʈ ����
        transform.LookAt(point);
    }
    private void Look()
    {
        // ����
        yRotation += lookDelta.x * cameraSensitivity * Time.deltaTime;
        // ������
        xRotation -= lookDelta.y * cameraSensitivity * Time.deltaTime;

        shootRoot.transform.rotation = Quaternion.Euler(xRotation + 6f, yRotation + 6f, 0);

        // �ִ�ġ
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        // ī�޶� ��Ʈ�� ȸ���ϴ½�����
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
