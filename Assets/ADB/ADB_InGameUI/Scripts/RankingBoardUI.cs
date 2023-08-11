using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingBoardUI : SceneUI
{
    private void Awake()
    {
        base.Awake();

    }
}

public struct PlayerRankingInfo
{
    public int rank;
    public string nickName;
    public int killCount;
}
