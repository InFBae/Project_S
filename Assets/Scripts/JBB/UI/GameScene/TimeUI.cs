using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JBB
{
    public class TimeUI : BaseUI
    {
        public static UnityEvent<int> OnLeftTimeChanged = new UnityEvent<int>();
        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            OnLeftTimeChanged.AddListener(SetTime);
        }

        private void OnDisable()
        {
            OnLeftTimeChanged.RemoveListener(SetTime);
        }

        public void SetTime(int time)
        {
            string min = (time / 60) < 10 ? $"0{time / 60}" : $"{time / 60}";
            string sec = (time % 60) < 10 ? $"0{time % 60}" : $"{time % 60}";

            texts["TimeText"].text = $"{min} : {sec}";                   
        }
    }
}

