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

        public void OnEnable()
        {
            StartCoroutine(CloseUIRoutine());
        }
        public void SetText(string text)
        {
            texts["Message"].text = text;
        }

        IEnumerator CloseUIRoutine()
        {
            yield return new WaitForSecondsRealtime(1f);
            CloseUI();
        }
   
    }
}

