using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : SceneUI
{
    private void Awake()
    {
        base.Awake();

        buttons["Setting"].onClick.AddListener(() => { SettingPopUpUI(); });

    }

    public void SettingPopUpUI()
    {
        // ���� �˾�â�� ������
        // GameManager.UI.ShowPopUpUI("UI/Image");   �̷��� Ǯ���ؼ� �˾�â ��������
        Debug.Log("�˾�â");
        GameManager.UI.ShowPopUpUI<PopUpUI>("UI/SettingPopUpUI");
    }
}
