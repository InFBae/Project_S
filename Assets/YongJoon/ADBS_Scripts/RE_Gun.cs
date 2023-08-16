using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using Photon.Pun;


public abstract class RE_Gun : MonoBehaviourPunCallbacks
{
    [SerializeField] protected Transform muzzlePos;
    [SerializeField] protected int fireDamage;
    [SerializeField] protected int remainBullet;             // ��ź ����
    [SerializeField] protected int availableBullet;       // ������ �ϱ� �� ��� ������ ��ź ���� 
    [SerializeField] protected int curAvailavleBullet;    // ��� ��� ���� ���� �Ѿ� ����   

    public abstract void Fire(Photon.Realtime.Player shooter, Vector3 shootPos, Vector3 shootDir);        // �Ѹ��� Fire ����� �ٸ��ϱ� abstract��
    public abstract void Reload();   // ������

    public bool IsBulletRemain()
    {
        return curAvailavleBullet > 0;
    }
}

