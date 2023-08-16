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
    [SerializeField] protected int remainBullet;             // 총탄 개수
    [SerializeField] protected int availableBullet;       // 재장전 하기 전 사용 가능한 총탄 개수 
    [SerializeField] protected int curAvailavleBullet;    // 계속 사용 중인 현재 총알 개수   

    public abstract void Fire(Photon.Realtime.Player shooter, Vector3 shootPos, Vector3 shootDir);        // 총마다 Fire 방식이 다르니까 abstract로
    public abstract void Reload();   // 재장전

    public bool IsBulletRemain()
    {
        return curAvailavleBullet > 0;
    }
}

