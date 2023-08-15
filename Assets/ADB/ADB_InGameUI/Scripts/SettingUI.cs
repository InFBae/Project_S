using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : SceneUI
{
    private void Awake()
    {
        base.Awake();

        buttons["Setting"].onClick.AddListener(() => { SettingPopUpUI(); });

        // ���ù�ư���� ���콺 Ŀ�� ����X UnityEngine.Cursor.lockState = CursorLockMode.None;
        
    }

    public void SettingPopUpUI()
    {
        // ���� �˾�â�� ������
        Debug.Log("Setting");
        GameManager.UI.ShowPopUpUI<PopUpUI>("UI/SettingPopUpUI");
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
}
