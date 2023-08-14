using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JBB
{
    public class RoomChattingPrefab : BaseUI
    {
        [SerializeField] TMP_InputField chatInput;
        public GameObject content;

        PhotonView pv;

        protected override void Awake()
        {
            base.Awake();
            
            pv = GetComponent<PhotonView>();
        }

        private void OnEnable()
        {
            chatInput.onEndEdit.AddListener(SendMessageRPC);
        }
        private void OnDisable()
        {
            chatInput.onEndEdit.RemoveListener(SendMessageRPC);
        }

        [PunRPC]
        public void CreateMessage(string text)
        {
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
    }
}

