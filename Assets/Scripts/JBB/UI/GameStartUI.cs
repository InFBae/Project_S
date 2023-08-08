using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JBB
{
    public class GameStartUI : SceneUI
    {
        private LogInUI logInUI;
        private SignInUI signInUI;

        protected override void Awake()
        {
            base.Awake();

            logInUI = GetComponentInChildren<LogInUI>();
            signInUI = GetComponentInChildren<SignInUI>();

            logInUI.gameObject.SetActive(false);
            signInUI.gameObject.SetActive(false);

            buttons["LogInButton"].onClick.AddListener(OnLogInButtonClicked);
            buttons["SignInButton"].onClick.AddListener(OnSignInButtonClicked);
            buttons["QuitButton"].onClick.AddListener(OnQuitButtonClicked);
        }

        public void OnLogInButtonClicked()
        {
            logInUI.gameObject.SetActive(true);
        }

        public void OnSignInButtonClicked()
        {
            signInUI.gameObject.SetActive(true);
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
            signInUI.gameObject.SetActive(false);
        }
    }
}

