using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JBB
{
    public class TargetKillSliderUI : BaseUI
    {
        [SerializeField] RankingBoardUI rankingBoardUI;
        [SerializeField] Slider firstSlider;
        [SerializeField] Slider secondSlider;
        protected override void Awake()
        {
            base.Awake();
        }

        public void InitTargetKillSlider()
        {
            texts["TargetKillText"].text = $"{PhotonNetwork.CurrentRoom.GetMaxKill()} Kill";
            firstSlider.maxValue = PhotonNetwork.CurrentRoom.GetMaxKill();
            secondSlider.maxValue = PhotonNetwork.CurrentRoom.GetMaxKill();

            UpdateSliderValue();
        }

        public void UpdateSliderValue()
        {
            Player first = rankingBoardUI.GetFirstPlayer();
            if (first != null)
            {
                firstSlider.value = first.GetKillCount();
                texts["FirstPlayerKill"].text = $"{first.GetKillCount()} Kill";
                texts["FirstPlayerName"].text = first.GetNickname();
            }
            else
            {
                firstSlider.gameObject.SetActive(false);
            }
            Player second = rankingBoardUI.GetSecondPlayer();
            if (second != null)
            {
                secondSlider.value = second.GetKillCount();
                texts["SecondPlayerKill"].text = $"{second.GetKillCount()} Kill";
                texts["SecondPlayerName"].text = second.GetNickname();
            }
            else
            {
                secondSlider.gameObject.SetActive(false);
            }
        }

    }
}

