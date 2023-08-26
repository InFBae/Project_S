using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace JBB
{
    public class SignUpUI : BaseUI
    {
        public static UnityEvent<string, string, string> OnSignUpClicked = new UnityEvent<string, string, string>();

        [SerializeField] TMP_InputField idInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_InputField passwordCheckInput;
        protected override void Awake()
        {
            base.Awake();

            buttons["ReturnButton"].onClick.AddListener(OnReturnButtonClicked);
            buttons["SignUpButton"].onClick.AddListener(OnSignUpButtonClicked);
        }

        public void OnReturnButtonClicked()
        {
            this.gameObject.SetActive(false);
        }

        public void OnSignUpButtonClicked()
        {
            OnSignUpClicked?.Invoke(idInput.text, passwordInput.text, passwordCheckInput.text);
        }
    }
}

