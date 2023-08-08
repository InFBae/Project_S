using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JBB
{
    public class FriendSlotUI : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void SetState(string nickname, int state)
        {
            texts["Nickname"].text = nickname;
            
            switch (state)
            {
                case 1:
                    break;
                case 2:
                    break;
            }
            texts["State"].text = state.ToString();
            
        }
        public string GetNickname()
        {
            return texts["Nickname"].ToString();
        }
    }
}

