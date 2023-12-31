using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADB_RE_DiePlayer : MonoBehaviourPun
{
    GameObject spawnPointsPrefab;
    Transform[] spawnPoints;

    [SerializeField] Camera cam;
    [SerializeField] AudioListener audioListener;
    PhotonView PV;


    private void Awake()
    {
        //gameObject.SetActive(false);
        PV = GetComponent<PhotonView>();
        spawnPointsPrefab = GameObject.FindWithTag("Respawn");
        spawnPoints = spawnPointsPrefab.GetComponentsInChildren<Transform>();
    }

    private void OnEnable()
    {
        if (PV.IsMine)
        {
            StartCoroutine(RespawnRoutine(5f));
        }
        else
        {
            cam.enabled = false;
            audioListener.enabled = false;
        }
    }

    IEnumerator RespawnRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        Transform spawnPoint = GetSpawnPoint();
        PhotonNetwork.Instantiate("AllInOnePlayerTest", spawnPoint.position, Quaternion.identity);
        PhotonNetwork.Destroy(gameObject);
    }
    public Transform GetSpawnPoint()
    {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, 8)];
        while (Physics.Raycast(spawnPoint.position, Vector3.up, 2))
        {
            spawnPoint = spawnPoints[UnityEngine.Random.Range(0, 8)];
        }
        return spawnPoint;
    }
}
