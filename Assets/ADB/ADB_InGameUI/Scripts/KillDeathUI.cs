using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillDeathUI : SceneUI
{
    public TMP_Text KillDeathTextUI;

    protected override void Awake()
    {
        base.Awake();
        KillDeathTextUI = texts["KillDeathText"];
    }

    private void Start()
    {
        KillDeathTextUI.text = "0 / 0";
    }

    public void ChagneKillDeathTextUI(int killCount, int DeathCount)
    {
        KillDeathTextUI.text = $"{killCount} / {DeathCount}";
    }
}