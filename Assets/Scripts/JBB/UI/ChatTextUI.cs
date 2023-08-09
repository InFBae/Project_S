using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class ChatTextUI : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void SetText(string text)
        {
            texts["ChatText"].text = text;
        }
    }
}

