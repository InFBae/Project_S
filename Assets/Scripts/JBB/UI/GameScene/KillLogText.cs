using JBB;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLogText : BaseUI
{
    protected override void Awake()
    {
        base.Awake();
    }
    public void SetKillLogText(bool isHeadShot, Player killed)
    {
        texts["HeadShot"].text = isHeadShot ? "[Head Shot]" : "";
        texts["Nickname"].text = killed.GetNickname();
    }

    public IEnumerator ReleaseRoutine(KillLogText text, float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Pool.ReleaseUI(text);
    }
}
