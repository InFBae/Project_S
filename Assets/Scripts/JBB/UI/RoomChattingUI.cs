using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JBB
{
    public class RoomChattingUI : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();
            
        }
        private void Start()
        {
            GameObject go = PhotonNetwork.Instantiate("UI/RoomChattingPrefab", transform.position, transform.rotation);
            go.transform.SetParent(transform, false);
            go.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }

    }
}

