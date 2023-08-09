using TMPro;
using UnityEngine;

namespace JBB
{
    public class ChattingUI : BaseUI
    {
        [SerializeField] TMP_InputField chatInput;
        [SerializeField] GameObject content;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            ChatManager.OnGetLobbyMessage.AddListener(CreateChatMessage);
            chatInput.onEndEdit.AddListener(OnEnterSend);
        }

        private void OnDisable()
        {
            ChatManager.OnGetLobbyMessage.RemoveListener(CreateChatMessage);
            chatInput.onEndEdit.RemoveListener(OnEnterSend);
        }

        public void OnEnterSend(string text)
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            {
                if (text == "")
                    return;
                GameManager.Chat.SendChatMessage(text);
                chatInput.text = "";
            }
        }

        private void CreateChatMessage(string inputLine)
        {
            ChatTextUI chatTextUI = GameManager.Pool.GetUI(GameManager.Resource.Load<ChatTextUI>("UI/ChatText"));
            chatTextUI.SetText(inputLine);
            chatTextUI.transform.SetParent(content.transform, false);
        }
    }
}
