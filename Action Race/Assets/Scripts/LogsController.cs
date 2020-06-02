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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        logsPanel.LogPlayerEnter(newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        logsPanel.LogPlayerLeave(otherPlayer.NickName);
    }
}
