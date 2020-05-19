using UnityEngine;
using Photon.Pun;

public class QuickPlayController : MonoBehaviourPunCallbacks
{
    [Header("Custom Scripts References")]
    [SerializeField] MainMenuPanel mainMenuPanel;
    [SerializeField] ConnectionStatusPanel connectionStatusPanel;

    public void QuickPlay()
    {
        StartCoroutine(connectionStatusPanel.MessageFadeIn(ConnectionStatus.Join));
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionStatusPanel.ChangeMessage(ConnectionStatus.JoinFail);

        StartCoroutine(connectionStatusPanel.MessageFadeOut());
        StartCoroutine(mainMenuPanel.TryCreateRoom());
    }
}
