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
        [SerializeField] protected Transform muzzlePos;
        [SerializeField] float damage;

        private void Start()
        {
        }

        public void ForwardDirection()
        {
            // ���� ī�޶�� ���� ������ �ٶ󺸵���
            // transform.localRotation = Camera.main.transform.localRotation;
        }

        public abstract void Fire();    // �Ѹ��� Fire ����� �ٸ��ϱ� abstract��
    }
}
