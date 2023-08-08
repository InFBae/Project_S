using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntry : MonoBehaviour
{
    private Player player;

    public void SetPlayer(Player player)
    {
        playerName.text =

    }

    public void Ready()
    {
        bool ready = player.GetReady();
        ready = !ready;
        player.SetReady(ready);
    }
}
