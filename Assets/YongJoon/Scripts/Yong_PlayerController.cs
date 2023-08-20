using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using System.Linq;

public class Yong_PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerColliderBody;
    [SerializeField] GameObject visibleBody;
    [SerializeField] GameObject GunHolder;
    [SerializeField] GameObject FPSBody;


    PhotonView PV;
    //PlayerAttacker PAttack;
    //PlayerMover PMover;
    //PlayerAimMove PAimMove;
    List<PlayerInput> inputList;
    List<Camera> cameraList;



    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        //PAttack = GetComponentInChildren<PlayerAttacker>();
        //PMover = GetComponentInChildren<PlayerMover>();
        //PAimMove = GetComponentInChildren<PlayerAimMove>();
        inputList = GetComponentsInChildren<PlayerInput>().ToList<PlayerInput>();
        cameraList = GetComponentsInChildren<Camera>().ToList<Camera>();

        //PV.RPC("TestRPC", RpcTarget.AllBufferedViaServer);


        if (!PV.IsMine)
        {
            foreach (Camera cam in cameraList)
            {
                //if (cam.GetComponent<AudioListener>() != null)
                //{
                //    cam.GetComponent<AudioListener>().enabled = false;
                //}
                cam.enabled = false;
            }
            foreach (PlayerInput value in inputList)
            {
                value.enabled = false;
            }
            foreach (Camera cam in cameraList)
            {
                if (cam.GetComponent<AudioListener>() != null)
                {
                    cam.GetComponent<AudioListener>().enabled = false;
                }
            }
            FPSBody.SetActive(false);
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                ChangeLayerRecursively(visibleBody, 10);
                ChangeLayerRecursively(GunHolder, 10);
                playerColliderBody.layer =  10;
            }
            else
            {
                ChangeLayerRecursively(visibleBody, 4);
                ChangeLayerRecursively(GunHolder, 4);
                playerColliderBody.layer = 4;
            }
            //visibleBody.SetActive(false);
        }

    }
    private void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, layer);
        }
    }

    private void Start()
    {

        //int myNum = PhotonNetwork.LocalPlayer.ActorNumber;
        //Debug.Log($"my Act Number is {myNum}");
    }
    //[PunRPC]
    //void TestRPC()
    //{
    //    if (!PV.IsMine)
    //    {

    //        //visibleBody.SetActive(false);
    //        ChangeLayerRecursively(visibleBody, 0);
    //        FPSBody.SetActive(false);
    //    }
    //    else
    //    {
    //        ChangeLayerRecursively(visibleBody, 4);
    //        FPSBody.SetActive(true);
    //    }
    //    //PAttack.enabled = false;
    //    //PMover.enabled = false;
    //    //PAimMove.enabled = false;
    //}
}
