using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ahndabi
{
    public class CheckKillUI : InGameUI
    {
        // Kill 하면 내가 kill한 사람의 닉네임이 중앙에 2초 동안 뜸

        [SerializeField] TMP_Text checkKillText;

        private void Awake()
        {
            base.Awake();

            checkKillText = texts["CheckKillText"];
        }

        private void Start()
        {
            gameObject.active = false;
        }

        public void AppearCheckKillText()
        {
            // 여기서 게임오브젝트 활성화, 킬당한 사람의 닉네임이 text에 적혀지고
        }
    }
}