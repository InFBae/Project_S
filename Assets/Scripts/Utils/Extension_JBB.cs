using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public static partial class Extension 
    {
        public static void CreatePopUpMessage(this UIManager ui, string text, float time)
        {
            PopUpMessage popUpMessage = GameManager.UI.ShowPopUpUI<PopUpMessage>("UI/PopUpMessage");
            popUpMessage.SetText(text);                      
            popUpMessage.SetTime(time);
        }
    }
}

