using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

// 커스텀 프로퍼티를 확장메서드로 사용
namespace JBB
{
    public static class CustomProperty
    {
        public static string GetNickname(this Player player)
        {
            PhotonHashtable property = player.CustomProperties;
            if (property.ContainsKey("Nickname"))
                return (string)property["Nickname"];
            else
                return "";
        }

        public static void SetNickname(this Player player, string nickname)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["Nickname"] = nickname;
            player.SetCustomProperties(property);
        }

        public static bool GetReady(this Player player)
        {
            PhotonHashtable property = player.CustomProperties;
            if (property.ContainsKey("Ready"))
                return (bool)property["Ready"];
            else
                return false;
        }

        public static void SetReady(this Player player, bool ready)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["Ready"] = ready;
            player.SetCustomProperties(property);
        }

        public static bool GetLoad(this Player player)
        {
            PhotonHashtable property = player.CustomProperties;
            if (property.ContainsKey("Load"))
                return (bool)property["Load"];
            else
                return false;
        }

        public static void SetLoad(this Player player, bool load)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["Load"] = load;
            player.SetCustomProperties(property);
        }

        public static int GetKillCount(this Player player)
        {
            PhotonHashtable property = player.CustomProperties;
            if (property.ContainsKey("KillCount"))
                return (int)property["KillCount"];
            else
                return 0;
        }

        public static void SetKillCount(this Player player, int killCount)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["KillCount"] = killCount;
            player.SetCustomProperties(property);
        }

        public static int GetDeathCount(this Player player)
        {
            PhotonHashtable property = player.CustomProperties;
            if (property.ContainsKey("DeathCount"))
                return (int)property["DeathCount"];
            else
                return 0;
        }

        public static void SetDeathCount(this Player player, int deathCount)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["DeathCount"] = deathCount;
            player.SetCustomProperties(property);
        }



        //-------------------- Room Property ------------------------//

        public static int GetLoadTime(this Room room)
        {
            PhotonHashtable property = room.CustomProperties;
            if (property.ContainsKey("LoadTime"))
                return (int)property["LoadTime"];
            else
                return -1;
        }

        public static void SetLoadTime(this Room room, int loadTime)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["LoadTime"] = loadTime;
            room.SetCustomProperties(property);
        }

        public static float GetMaxKill(this Room room)
        {
            PhotonHashtable property = room.CustomProperties;
            if (property.ContainsKey("MaxKill"))
                return (int)property["MaxKill"];
            else
                return -1;
        }
        public static void SetMaxKill(this Room room, int maxKill)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["MaxKill"] = maxKill;
            room.SetCustomProperties(property);
        }
        public static string GetGameType(this Room room)
        {
            PhotonHashtable property = room.CustomProperties;
            if (property.ContainsKey("GameType"))
                return (string)property["GameType"];
            else
                return "";
        }

        public static void SetGameType(this Room room, string gameType)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["GameType"] = gameType;
            room.SetCustomProperties(property);
        }

        public static float GetGameTime(this Room room)
        {
            PhotonHashtable property = room.CustomProperties;
            if (property.ContainsKey("GameTime"))
                return (float)property["GameTime"];
            else
                return 20;
        }

        public static void SetGameTime(this Room room, float gameTime)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["GameTime"] = gameTime;
            room.SetCustomProperties(property);
        }

        public static bool GetIntrusion(this Room room)
        {
            PhotonHashtable property = room.CustomProperties;
            if (property.ContainsKey("Intrusion"))
                return (bool)property["Intrusion"];
            else
                return false;
        }

        public static void SetIntrusion(this Room room, bool intrusion)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["Intrusion"] = intrusion;
            room.SetCustomProperties(property);
        }

        public static bool GetIsPlaying(this Room room)
        {
            PhotonHashtable property = room.CustomProperties;
            if (property.ContainsKey("IsPlayingNow"))
                return (bool)property["IsPlayingNow"];
            else
                return false;
        }

        public static void SetIsPlaying(this Room room, bool isPlaying)
        {
            PhotonHashtable property = new PhotonHashtable();
            property["IsPlaying"] = isPlaying;
            room.SetCustomProperties(property);
        }

    }
}
