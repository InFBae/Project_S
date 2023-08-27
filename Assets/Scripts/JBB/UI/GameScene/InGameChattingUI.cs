using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JBB
{
    public class InGameChattingUI : BaseUI
    {
        public TMP_InputField chatInput;
        public GameObject content;
        public PhotonView myPV;
        protected override void Awake()
        {
            base.Awake();
        }

        public void OnEnter(InputValue input)
        {
            if (myPV == null)
            {
                PhotonView[] pvList = FindObjectsOfType<PhotonView>();
                foreach (PhotonView pv in pvList)
                {
                    if (pv.IsMine)
                    {
                        myPV = pv;
                    }
                }
            }
            if (chatInput.isFocused)
            {
                if (chatInput == null)
                {
                    chatInput = FindObjectOfType<RoomChattingUI>().GetComponent<RoomChattingUI>().chatInput;
                }
                if (chatInput.text != "")
                {
                    SendMessageRPC(chatInput.text);
                }
            }
            else
            {
                myPV.gameObject.GetComponent<PlayerInput>().enabled = false;
                chatInput.Select();
            }
                 
        }

        public void SendMessageRPC(string text)
        {
            string chatText = $"{GameManager.Chat.Nickname} : {text}";
            myPV.RPC("CreateMessage", RpcTarget.All, chatText);
            chatInput.text = "";
        }
    }
}
