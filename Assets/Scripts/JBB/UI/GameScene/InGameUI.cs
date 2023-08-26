using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JBB
{
    public class InGameUI : BaseUI
    {
        KillDeathUI killDeathUI;
        RankingBoardUI rankingBoardUI;
        StatusUI statusUI;
        KillLogUI killLogUI;
        TargetKillSliderUI targetKillSliderUI;
        InGameSettingUI inGameSettingUI;

        protected override void Awake()
        {
            base.Awake();

            killDeathUI = GetComponentInChildren<KillDeathUI>();
            rankingBoardUI = GetComponentInChildren<RankingBoardUI>();
            statusUI = GetComponentInChildren<StatusUI>();
            killLogUI = GetComponentInChildren<KillLogUI>();
            targetKillSliderUI = GetComponentInChildren<TargetKillSliderUI>();
            inGameSettingUI = GetComponentInChildren<InGameSettingUI>();
            inGameSettingUI.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (inGameSettingUI.isActiveAndEnabled)
                {
                    inGameSettingUI.Cancel();
                }
                else
                {
                    inGameSettingUI.gameObject.SetActive(true);
                }                              
            }
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

