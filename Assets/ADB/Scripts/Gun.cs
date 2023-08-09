using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace ahndabi
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected Transform muzzlePos;
        [SerializeField] protected int fireDamage;
        [SerializeField] protected int remainBullet;             // ��ź ����
        [SerializeField] protected int availableBullet;       // ������ �ϱ� �� ��� ������ ��ź ���� 
        [SerializeField] protected int curAvailavleBullet;    // ��� ��� ���� ���� �Ѿ� ����   
        [SerializeField] protected StatusUI statusUI;

        public void ForwardDirection()
        {
            // ���� ī�޶�� ���� ������ �ٶ󺸵���
            // transform.localRotation = Camera.main.transform.localRotation;
        }

        public abstract void Fire();        // �Ѹ��� Fire ����� �ٸ��ϱ� abstract��
        public abstract void Reload();   // ������
    }
}
