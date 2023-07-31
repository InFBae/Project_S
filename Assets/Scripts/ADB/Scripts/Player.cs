using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어가 가지고 있는 것들

    [SerializeField] protected float hp;

    private void Start()
    {
        hp = 200f;
    }

}
