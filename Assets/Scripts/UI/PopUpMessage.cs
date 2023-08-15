using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class PopUpMessage : PopUpUI
    {
        protected override void Awake()
        {
            base.Awake();
        }
        public void SetTime(float time)
        {
            Time.timeScale = 1;
            StartCoroutine(CloseUIRoutine(time));
        }
        public void SetText(string text)
        {
            texts["Message"].text = text;
        }

        IEnumerator CloseUIRoutine(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            CloseUI();
        }
   
    }
}

