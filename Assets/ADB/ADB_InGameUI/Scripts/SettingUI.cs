using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : SceneUI
{
    private void Awake()
    {
        base.Awake();

        buttons["Setting"].onClick.AddListener(() => { SettingPopUpUI(); });

        // 세팅버튼에는 마우스 커서 잠기면X UnityEngine.Cursor.lockState = CursorLockMode.None;
        
    }

    public void SettingPopUpUI()
    {
        // 세팅 팝업창이 떠야함
        Debug.Log("Setting");
        GameManager.UI.ShowPopUpUI<PopUpUI>("UI/SettingPopUpUI");
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
}
