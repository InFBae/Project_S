using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace JBB
{
    public class SignInUI : BaseUI
    {
        public static UnityEvent<string, string, string> OnSignInClicked = new UnityEvent<string, string, string>();

        [SerializeField] TMP_InputField idInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_InputField passwordCheckInput;
        protected override void Awake()
        {
            base.Awake();

            buttons["ReturnButton"].onClick.AddListener(OnReturnButtonClicked);
            buttons["SignInButton"].onClick.AddListener(OnSignInButtonClicked);
        }

        public void OnReturnButtonClicked()
        {
            this.gameObject.SetActive(false);
        }

        public void OnSignInButtonClicked()
        {
            OnSignInClicked?.Invoke(idInput.text, passwordInput.text, passwordCheckInput.text);
        }
    }
}

