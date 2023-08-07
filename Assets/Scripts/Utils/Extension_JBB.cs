using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public static partial class Extension 
    {
        public static void CreatePopUpMessage(this UIManager ui, string text)
        {
            PopUpMessage popUpMessage = GameManager.UI.ShowPopUpUI<PopUpMessage>("UI/PopUpMessage");
            popUpMessage.SetText(text);                      
        }
    }
}

