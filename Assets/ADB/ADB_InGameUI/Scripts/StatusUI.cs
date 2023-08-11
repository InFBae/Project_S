using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUI : SceneUI
{
    public TMP_Text HpTextUI;
    public TMP_Text CurrentBulletUI;
    public TMP_Text RemainBulletUI;
    protected override void Awake()
    {
        base.Awake();
        HpTextUI = texts["HPValue"];
        CurrentBulletUI = texts["CurrentBullet"];
        RemainBulletUI = texts["RemainBullet"];
    }

    public void DecreaseHPUI(int damage)
    {
        // damage��ŭ text ���� 
        HpTextUI.text = (int.Parse(HpTextUI.text) - damage).ToString();
        if (int.Parse(HpTextUI.text) <= 0)
            HpTextUI.text = "0";
    }

    public void DecreaseCurrentBulletUI(int curAvailavleBullet)
    {
        // źâ �� ����
        CurrentBulletUI.text = curAvailavleBullet.ToString();
    }

    public void DecreaseRemainBulletUI(int remainBullet)
    {
        // ���� �� �Ѿ� ����
        RemainBulletUI.text = remainBullet.ToString();
    }
}