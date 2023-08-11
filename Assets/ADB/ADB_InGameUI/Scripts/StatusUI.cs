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
        // damage¸¸Å­ text º¯µ¿ 
        HpTextUI.text = (int.Parse(HpTextUI.text) - damage).ToString();
        if (int.Parse(HpTextUI.text) <= 0)
            HpTextUI.text = "0";
    }

    public void DecreaseCurrentBulletUI(int curAvailavleBullet)
    {
        // ÅºÃ¢ ¼ö º¯µ¿
        CurrentBulletUI.text = curAvailavleBullet.ToString();
    }

    public void DecreaseRemainBulletUI(int remainBullet)
    {
        // ³²Àº ÃÑ ÃÑ¾Ë °³¼ö
        RemainBulletUI.text = remainBullet.ToString();
    }
}