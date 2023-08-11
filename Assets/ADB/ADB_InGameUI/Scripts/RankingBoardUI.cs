using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class RankingBoardUI : SceneUI
{
    public TMP_Text rankUI;
    public TMP_Text playerNickNameUI;
    public TMP_Text playerKillUI;

    private void Awake()
    {
        base.Awake();

        rankUI = texts["Rank"];
        playerNickNameUI = texts["PlayerName"];
        playerKillUI = texts["PlayerKill"];
    }
}

[SerializeField]
public struct PlayerRankingInfo
{
    public int rank;
    public string nickName;
    public int killCount;
}
