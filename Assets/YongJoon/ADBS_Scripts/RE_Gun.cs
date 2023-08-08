using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace Yong
{
    public abstract class RE_Gun : MonoBehaviour
    {
        [SerializeField] protected Transform muzzlePos;
        [SerializeField] protected float fireDamage;
        [SerializeField] protected int allBullet;             // ��ź ����
        [SerializeField] protected int availableBullet;       // ������ �ϱ� �� ��� ������ ��ź ���� 
        [SerializeField] protected int curAvailavleBullet;    // ��� ��� ���� ���� �Ѿ� ����   

        public void ForwardDirection()
        {
            // ���� ī�޶�� ���� ������ �ٶ󺸵���
            // transform.localRotation = Camera.main.transform.localRotation;
        }

        public abstract void Fire();        // �Ѹ��� Fire ����� �ٸ��ϱ� abstract��
        public abstract void Reload();   // ������
    }
}
