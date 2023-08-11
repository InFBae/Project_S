using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JBB
{
    public class LogInUI : BaseUI
    {
        public static UnityEvent<string, string> OnLogInClicked = new UnityEvent<string, string>();

        [SerializeField] TMP_InputField idInput;
        [SerializeField] TMP_InputField passwordInput;

        protected override void Awake()
        {
            base.Awake();

            buttons["ReturnButton"].onClick.AddListener(OnReturnButtonClicked);
            buttons["LogInButton"].onClick.AddListener(OnLogInButtonClicked);           
        }

        public void OnReturnButtonClicked()
        {
            this.gameObject.SetActive(false);
        }
        
        public void OnLogInButtonClicked()
        {
            OnLogInClicked?.Invoke(idInput.text, passwordInput.text);
        }

    }
}

