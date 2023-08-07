using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerAimMove : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] Transform realAimTarget;
    [SerializeField] Transform playerAimTarget;
    [SerializeField] float cameraSensitivity;
    [SerializeField] float lookDistance;

    private Transform curRoot;
    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;

    private void Awake()
    {
        curRoot = cameraRoot;
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
        // �����ִ� ��ü�� ��ġ
        Vector3 lookPoint = Camera.main.transform.position + Camera.main.transform.forward * lookDistance;
        realAimTarget.position = lookPoint;
        playerAimTarget.position = new Vector3(playerAimTarget.position.x, lookPoint.y, playerAimTarget.position.z);
        // �����ִ� ������ �������� ������ ������ 
        lookPoint.y = transform.position.y;
        // ������Ʈ ����
        transform.LookAt(lookPoint);
    }
    private void Look()
    {
        // ����
        yRotation += lookDelta.x * cameraSensitivity * Time.deltaTime;
        // ������
        xRotation -= lookDelta.y * cameraSensitivity * Time.deltaTime;
        // �ִ�ġ
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        // ī�޶� ��Ʈ�� ȸ���ϴ½�����
        cameraRoot.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void OnAim(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
