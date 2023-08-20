using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class InGameUI : BaseUI
    {
        KillDeathUI killDeathUI;
        RankingBoardUI rankingBoardUI;
        StatusUI statusUI;
        KillLogUI killLogUI;
        TargetKillSliderUI targetKillSliderUI;


        protected override void Awake()
        {
            base.Awake();

            killDeathUI = GetComponentInChildren<KillDeathUI>();
            rankingBoardUI = GetComponentInChildren<RankingBoardUI>();
            statusUI = GetComponentInChildren<StatusUI>();
            killLogUI = GetComponentInChildren<KillLogUI>();
            targetKillSliderUI = GetComponentInChildren<TargetKillSliderUI>();

        }

        public void InitUI()
        {
            rankingBoardUI.UpdateRankerList();
            rankingBoardUI.UpdateRankingBoard();
            targetKillSliderUI.InitTargetKillSlider();
            killDeathUI.UpdateKillDeathText();
        }

        public void UpdateKillDeathUI()
        {
            killDeathUI.UpdateKillDeathText();
        }

        public void UpdateRankingBoard()
        {
            rankingBoardUI.UpdateRankerList();
            rankingBoardUI.UpdateRankingBoard();
        }

        public void UpdateTargetKillSliderValue()
        {
            targetKillSliderUI.UpdateSliderValue();
        }

        public int GetFirstPlayerKill()
        {
            return rankingBoardUI.GetFirstPlayer().GetKillCount();
        }
    }
}

