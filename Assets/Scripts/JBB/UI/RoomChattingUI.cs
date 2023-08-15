using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JBB
{
    public class RoomChattingUI : BaseUI
    {
        public TMP_InputField chatInput;
        public GameObject content;
        protected override void Awake()
        {
            base.Awake();            
        }
        private void Start()
        {
            GameObject go = PhotonNetwork.Instantiate("Chatter", Vector3.zero, Quaternion.identity);
            Chatter chatter = go.GetComponent<Chatter>();
        }

    }
}

