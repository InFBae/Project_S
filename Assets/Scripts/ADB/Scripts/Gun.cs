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
            // 총이 카메라와 같은 방향을 바라보도록
            // transform.localRotation = Camera.main.transform.localRotation;
        }

        public abstract void Fire();    // 총마다 Fire 방식이 다르니까 abstract로
    }
}
