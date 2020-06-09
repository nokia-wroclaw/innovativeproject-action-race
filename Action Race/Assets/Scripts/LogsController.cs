using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(LogsPanel))]
public class LogsController : MonoBehaviourPunCallbacks
{
    LogsPanel logsPanel;

    void Awake()
    {
        logsPanel = GetComponent<LogsPanel>();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        logsPanel.LogPlayerEnter(newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        logsPanel.LogPlayerLeave(otherPlayer.NickName);
    }
}
