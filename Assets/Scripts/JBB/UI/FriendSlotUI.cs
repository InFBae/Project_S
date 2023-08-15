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
            texts["Nickname"].color = Color.white;
            texts["State"].color = Color.white;

            string _state;
            switch (state)
            {
                case 1:
                    _state = "Invisible";
                    break;
                case 2:
                    _state = "<b>Online</b>";
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
                    texts["Nickname"].color = Color.gray;
                    texts["State"].color = Color.gray;
                    break;
            }
            texts["Nickname"].text = nickname;
            texts["State"].text = _state.ToString();
            
        }
        public string GetNickname()
        {
            return texts["Nickname"].text.ToString();
        }
    }
}

