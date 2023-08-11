using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public static class ADB_CustomProperty
{
    // RankingBoard���� ������ ���� �Լ���
    public static int GetKillCount(this Player player)
    {
        Hashtable property = player.CustomProperties;
        if (property.ContainsKey("KillCount"))
            return (int)property["KillCount"];
        else
            return 11111111;
    }

    public static void SetKillCount(this Player player, int killCount)
    {
        Hashtable property = player.CustomProperties;
        property["KillCount"] = killCount;
        player.SetCustomProperties(property);
    }

}
