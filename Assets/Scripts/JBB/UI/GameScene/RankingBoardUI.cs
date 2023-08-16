using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player = Photon.Realtime.Player;

namespace JBB
{
    public class RankingBoardUI : BaseUI
    {
        Player[] rankers = new Player[5];
        protected override void Awake()
        {
            base.Awake();
        }

        public void UpdateRankerList()
        {
            Player[] currentPlayers = PhotonNetwork.PlayerList;
            Array.Sort(currentPlayers, ((Player a, Player b) => { return a.GetKillCount() > b.GetKillCount() ? -1 : 1;}));

            for (int i = 0; i < rankers.Length; i++)
            {
                if (i < currentPlayers.Length)
                {
                    rankers[i] = currentPlayers[i];
                }                
            }                                   
        }
        public void UpdateRankingBoard()
        {
            for(int i = 0; i < rankers.Length; i++)
            {
                if (rankers[i] != null)
                {
                    texts[$"Rank{i + 1}"].text = $"{i + 1}. ";
                    texts[$"PlayerName{i + 1}"].text = rankers[i].GetNickname();
                    texts[$"PlayerKill{i + 1}"].text = $"{rankers[i].GetKillCount()} Kill";
                }
                else
                {
                    texts[$"Rank{i + 1}"].text = "";
                    texts[$"PlayerName{i + 1}"].text = "";
                    texts[$"PlayerKill{i + 1}"].text = "";
                }
            }
        }

        public Player GetFirstPlayer()
        {
            return rankers[0];
        }

        public Player GetSecondPlayer() 
        { 
            return rankers[1]; 
        }
    }
}

