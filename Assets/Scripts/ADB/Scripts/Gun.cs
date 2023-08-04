using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace ahndabi
{
    public abstract class Gun : MonoBehaviour
    {
        public UnityEvent OnFire;
        [SerializeField] protected Animator anim;
        [SerializeField] protected Transform muzzlePos;
        [SerializeField] protected float fireDamage;
        [SerializeField] protected int allBullet;             // ��ź ����
        [SerializeField] protected int availableBullet;       // ������ �ϱ� �� ��� ������ ��ź ���� 
        [SerializeField] protected int curAvailavleBullet;    // ��� ��� ���� ���� �Ѿ� ����   
        

        private void Awake()
        {
            // anim = GetComponentInParent<Animator>();
            // �÷��̾��� ���ӿ�����Ʈ�� �ҷ��ͼ� �� �÷��̾��� GetComponent<�ִϸ�����>�� �ϸ� ��.
        }

        private void Start()
        {
        }

        public void ForwardDirection()
        {
            // ���� ī�޶�� ���� ������ �ٶ󺸵���
            // transform.localRotation = Camera.main.transform.localRotation;
        }

        public abstract void Fire();        // �Ѹ��� Fire ����� �ٸ��ϱ� abstract��
        public abstract void Reload();   // ������
    }
}
