using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �÷��̾ ������ �ִ� �͵�

    [SerializeField] protected float hp;

    private void Start()
    {
        hp = 200f;
    }

}
