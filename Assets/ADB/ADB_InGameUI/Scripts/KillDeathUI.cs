using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ahndabi
{
    public class KillDeathUI : SceneUI
    {

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            texts["KillDeathText"].text = "0 / 0";
        }

        private void OnEnable()
        {

        }

        public void ChagneKillDeathTextUI(int killCount, int DeathCount)
        {
            texts["KillDeathText"].text = $"{killCount} / {DeathCount}";
        }
    }
}