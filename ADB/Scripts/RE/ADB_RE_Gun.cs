using ahndabi;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using ahndabi;


public abstract class ADB_RE_Gun : MonoBehaviour
{
    [SerializeField] protected Transform muzzlePos;
    [SerializeField] protected int fireDamage;
    [SerializeField] protected int remainBullet;             // ��ź ����
    [SerializeField] protected int availableBullet;       // ������ �ϱ� �� ��� ������ ��ź ���� 
    [SerializeField] protected int curAvailavleBullet;    // ��� ��� ���� ���� �Ѿ� ����
    protected Photon.Realtime.Player player;
    public RaycastHit hit;

    public abstract void Fire();        // �Ѹ��� Fire ����� �ٸ��ϱ� abstract��
    public abstract void Reload();   // ������

    public bool IsBulletRemain()
    {
        return curAvailavleBullet > 0;
    }
}

