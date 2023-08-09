using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiePlayer : MonoBehaviour
{
    // Player의 transform와 계속 똑같아야 함

    [SerializeField] Transform Player;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
       StartCoroutine(ActiveFalseRoutine());
    }

    private void LateUpdate()
    {
        gameObject.transform.position = Player.transform.position;
        gameObject.transform.rotation = Player.transform.rotation;
    }

    IEnumerator ActiveFalseRoutine()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
