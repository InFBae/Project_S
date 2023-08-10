using ahndabi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiePlayer : MonoBehaviour
{
    // Player의 transform와 계속 똑같아야 함

    [SerializeField] Transform player;
    [SerializeField] SpawnPlayer spawnPlayer;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
       StartCoroutine(SpawnRoutine());
       StartCoroutine(ActiveFalseRoutine());
    }

    private void LateUpdate()
    {
        gameObject.transform.position = player.transform.position;
        gameObject.transform.rotation = player.transform.rotation;
    }

    IEnumerator ActiveFalseRoutine()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        spawnPlayer.Spawn();
        player.parent.gameObject.SetActive(true);
    }
}
