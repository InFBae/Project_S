using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

// 커스텀 프로퍼티를 확장메서드로 사용
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

    public static void SetNick(this Player player, string nick)
    {
        PhotonHashtable property = new PhotonHashtable();
        property["Nickname"] = nick;
        player.SetCustomProperties(property);
    }

    public static string GetNick(this Player player)
    {
        PhotonHashtable property = player.CustomProperties;
        if (property.ContainsKey("Nickname"))
            return (string)property["Nickname"];
        else
            return "";
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

    //room customproperty 받아 오는 법
    public static float GetRoomInfo_maxKill(this Room room)
    {
        PhotonHashtable property = room.CustomProperties;
        if (property.ContainsKey("maxKill"))
            return (int)property["maxKill"];
        else
            return -1;
    }
    public static string GetRoomInfo_gameTime(this Room room)
    {
        PhotonHashtable property = room.CustomProperties;
        if (property.ContainsKey("gameTime"))
            return (string)property["gameTime"];
        else
            return "";
    }
    public static bool GetRoomInfo_canIrrupt(this Room room)
    {
        PhotonHashtable property = room.CustomProperties;
        if (property.ContainsKey("CanIrrupt"))
            return (bool)property["CanIrrupt"];
        else
            return false;
    }
}