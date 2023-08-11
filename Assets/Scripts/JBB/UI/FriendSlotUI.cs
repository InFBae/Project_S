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

            string _state;
            switch (state)
            {
                case 1:
                    _state = "Invisible";
                    break;
                case 2:
                    _state = "Online";
                    break;
                case 3:
                    _state = "Away";
                    break;
                case 4:
                    _state = "Do not disturb";
                    break;
                case 5:
                    _state = "Looking For Game/Group";
                    break;
                case 6:
                    _state = "Playing";
                    break;
                default:
                    _state = "Offline";
                    break;
            }
            texts["State"].text = _state.ToString();
            
        }
        public string GetNickname()
        {
            return texts["Nickname"].text.ToString();
        }
    }
}

