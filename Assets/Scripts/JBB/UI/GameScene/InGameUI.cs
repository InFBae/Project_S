using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class InGameUI : BaseUI
    {
        TimeUI timeUI;
        KillDeathUI killDeathUI;
        RankingBoardUI rankingBoardUI;
        StatusUI statusUI;
        KillLogUI killLogUI;
        TargetKillSliderUI targetKillSliderUI;


        protected override void Awake()
        {
            base.Awake();

            timeUI = GetComponentInChildren<TimeUI>();
            killDeathUI = GetComponentInChildren<KillDeathUI>();
            rankingBoardUI = GetComponentInChildren<RankingBoardUI>();
            statusUI = GetComponentInChildren<StatusUI>();
            killLogUI = GetComponentInChildren<KillLogUI>();
            targetKillSliderUI = GetComponentInChildren<TargetKillSliderUI>();

        }

        public void UpdateKillDeathUI()
        {
            killDeathUI.UpdateKillDeathText();
        }
    }
}

