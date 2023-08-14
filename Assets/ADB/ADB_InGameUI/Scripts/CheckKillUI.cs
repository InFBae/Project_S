using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ahndabi
{
    public class CheckKillUI : InGameUI
    {
        // Kill �ϸ� ���� kill�� ����� �г����� �߾ӿ� 2�� ���� ��

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
            // ���⼭ ���ӿ�����Ʈ Ȱ��ȭ, ų���� ����� �г����� text�� ��������
        }
    }
}