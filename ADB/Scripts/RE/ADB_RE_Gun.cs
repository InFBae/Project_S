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
    [SerializeField] protected int remainBullet;             // 총탄 개수
    [SerializeField] protected int availableBullet;       // 재장전 하기 전 사용 가능한 총탄 개수 
    [SerializeField] protected int curAvailavleBullet;    // 계속 사용 중인 현재 총알 개수
    protected Photon.Realtime.Player player;
    public RaycastHit hit;

    public abstract void Fire();        // 총마다 Fire 방식이 다르니까 abstract로
    public abstract void Reload();   // 재장전

    public bool IsBulletRemain()
    {
        return curAvailavleBullet > 0;
    }
}

