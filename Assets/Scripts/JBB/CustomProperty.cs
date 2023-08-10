using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

// Ŀ���� ������Ƽ�� Ȯ��޼���� ���
namespace JBB
{
    public static class CustomProperty
    {
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

        public static string GetGameTime(this Room room)
        {
            PhotonHashtable property = room.CustomProperties;
            if (property.ContainsKey("GameTime"))
                return (string)property["GameTime"];
            else
                return "";
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