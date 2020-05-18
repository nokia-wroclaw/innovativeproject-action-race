using UnityEngine;
using Photon.Pun;

public class QuickPlayController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject mainMenuPanelGO;
    [SerializeField] GameObject createRoomPanelGO;

    ConnectionStatusPanel csp;

    void Awake()
    {
        csp = FindObjectOfType<ConnectionStatusPanel>();
    }

    public void QuickPlay()
    {
        StartCoroutine(csp.MessageFadeIn(ConnectionStatus.Join));
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        csp.ChangeMessage(ConnectionStatus.JoinFail);
        StartCoroutine(csp.MessageFadeOut());

        mainMenuPanelGO.SetActive(false);
        createRoomPanelGO.SetActive(true);
    }
}
