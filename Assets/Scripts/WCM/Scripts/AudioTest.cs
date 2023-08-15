using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioTest : MonoBehaviourPunCallbacks
{
    PhotonView PV;

    public AudioClip fireClip;
    public AudioClip footClip;

    public Transform muzzlePoint;
    public Transform footPoint;

    private void Start()
    {
    }

    public void Update()
    {
        //SoundGenerator();
    }

    [PunRPC]
    private void FireSound()
    {
        AudioSource.PlayClipAtPoint(fireClip, muzzlePoint.position);
    }

    [PunRPC]
    private void MoveSound()
    {
        AudioSource.PlayClipAtPoint(footClip, footPoint.position);
    }
}
