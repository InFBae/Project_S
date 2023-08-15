using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JBB
{
    public class Chatter : MonoBehaviourPun
    {
        public TMP_InputField chatInput;
        public GameObject content;

        PhotonView pv;
        PlayerInput input;

        private void Awake()
        {
            pv = GetComponent<PhotonView>();
            input = GetComponent<PlayerInput>();
        }

        private void Start()
        {/*
            if (!pv.IsMine)
            {
                input.enabled = false;
            }*/

            chatInput = FindObjectOfType<RoomChattingUI>().GetComponent<RoomChattingUI>().chatInput;
            content = FindObjectOfType<RoomChattingUI>().GetComponent<RoomChattingUI>().content;                       
        }

        [PunRPC]
        public void CreateMessage(string text)
        {
            if (content == null)
            {
                content = FindObjectOfType<RoomChattingUI>().GetComponent<RoomChattingUI>().content;
            }
            ChatTextUI chat = GameManager.Pool.GetUI(GameManager.Resource.Load<ChatTextUI>("UI/ChatText"));
            chat.SetText(text);
            chat.transform.SetParent(content.transform, false);
        }

        public void SendMessageRPC(string text)
        {
            string chatText = $"{GameManager.Chat.Nickname} : {text}";
            pv.RPC("CreateMessage", RpcTarget.All, chatText);
            chatInput.text = "";
        }

        public void OnEnter(InputValue input)
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
    }
}


