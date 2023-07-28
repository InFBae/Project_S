using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Ahndabi
{
    public class Poolable : MonoBehaviour
    {
        // 빌려지는 애들

        [SerializeField] private bool autoRelease;
        [SerializeField] private float releaseTime;

        private ObjectPool<GameObject> pool;
        public ObjectPool<GameObject> Pool { get { return pool; } set { pool = value; } }

        private void OnEnable()
        {
            if (autoRelease)
                releaseRoutine = StartCoroutine(ReleaseRoutine());
        }

        public void Release()
        {
            if (releaseRoutine != null)
                StopCoroutine(releaseRoutine);
            if (pool != null)
                pool.Release(this.gameObject);
        }

        Coroutine releaseRoutine;
        IEnumerator ReleaseRoutine()
        {
            yield return new WaitForSeconds(releaseTime);
            if (pool != null)
                pool.Release(this.gameObject);
        }
    }
}

