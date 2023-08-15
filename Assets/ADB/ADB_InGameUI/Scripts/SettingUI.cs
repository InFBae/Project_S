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
        // 세팅 팝업창이 떠야함
        // GameManager.UI.ShowPopUpUI("UI/Image");   이렇게 풀링해서 팝업창 가져오기
        Debug.Log("팝업창");
        GameManager.UI.ShowPopUpUI<PopUpUI>("UI/SettingPopUpUI");
    }
}
