using JetBrains.Annotations;
using Photon.Pun;
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
                    foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                    {
                        if (player.IsLocal)
                        {
                            inGameSettingUI.gameObject.SetActive(true);
                            UnityEngine.Cursor.visible = true;
                            UnityEngine.Cursor.lockState = CursorLockMode.None;
                            //gameObject.GetComponent<PlayerInput>().enabled = false;     // Fire 되면 안됨. PlayerInput을 비활성화
                        }
                    }
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

