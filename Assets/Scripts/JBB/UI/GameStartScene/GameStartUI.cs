using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JBB
{
    public class GameStartUI : SceneUI
    {
        private LogInUI logInUI;
        private SignUpUI signUpUI;

        protected override void Awake()
        {
            base.Awake();

            logInUI = GetComponentInChildren<LogInUI>();
            signUpUI = GetComponentInChildren<SignUpUI>();

            logInUI.gameObject.SetActive(false);
            signUpUI.gameObject.SetActive(false);

            buttons["LogInButton"].onClick.AddListener(OnLogInButtonClicked);
            buttons["SignUpButton"].onClick.AddListener(OnSignInButtonClicked);
            buttons["QuitButton"].onClick.AddListener(OnQuitButtonClicked);
        }

        public void OnLogInButtonClicked()
        {
            logInUI.gameObject.SetActive(true);
        }

        public void OnSignInButtonClicked()
        {
            signUpUI.gameObject.SetActive(true);
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        public void CloseLogInUI()
        {
            logInUI.gameObject.SetActive(false);
        }
        public void CloseSignInUI()
        {
            signUpUI.gameObject.SetActive(false);
        }
    }
}

