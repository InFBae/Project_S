using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ahndabi
{
    public class LobbyGameSettingUI : PopUpUI
    {
        // �����, ȿ����, ���콺 ����

        private void Awake()
        {
            base.Awake();

            buttons["ReturnButton"].onClick.AddListener(() => { CloseUI(); });
            buttons["ApplyButton"].onClick.AddListener(() => { Debug.Log("Apply"); });
        }

        public void MouseSensitivityControl(float sensitivity)
        {
            InGameSettingPopUpUI.OnMouseSensiticityControl?.Invoke(sliders["MouseSensitivitySlider"].value);
        }

        public void BackGroundSoundControl(float volumeValue)
        {

        }

        public void EffectSoundControl(float volumeValue)
        {

        }
    }
}
