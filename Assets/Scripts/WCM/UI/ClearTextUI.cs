using JBB;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTextUI : BaseUI
{
    [SerializeField] RankingBoardUI rankingBoardUI;
    protected override void Awake()
    {
        base.Awake();

    }

    private void OnEnable()
    {
        for(int i = 0; i < rankingBoardUI.rankers.Length; i++)
        {
            if (rankingBoardUI.rankers[i].IsLocal)
            {
                texts["Rank"].text = (i+1).ToString();
            }
        }
        
    }
}
