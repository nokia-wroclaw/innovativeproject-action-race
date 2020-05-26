using UnityEngine;
using Photon.Pun;

public class QuickPlayController : MonoBehaviourPunCallbacks
{
    [Header("Custom Scripts References")]
    [SerializeField] MainMenuPanel mainMenuPanel;
    [SerializeField] ConnectionStatusPanel connectionStatusPanel;
    [SerializeField] JoinRoomController joinRoomController;

    public void QuickPlay()
    {
        StartCoroutine(connectionStatusPanel.MessageFadeIn(ConnectionStatus.Join));
        //PhotonNetwork.JoinRandomRoom();

        string roomName = joinRoomController.GetRandomRoomName();
        if(roomName == null)
        {
            connectionStatusPanel.ChangeMessage(ConnectionStatus.JoinFail);

            StartCoroutine(connectionStatusPanel.MessageFadeOut());
            StartCoroutine(mainMenuPanel.TryCreateRoom());
        }
        else
            PhotonNetwork.JoinRoom(roomName);
    }

    //public override void OnJoinRandomFailed(short returnCode, string message)
    //{
    //    connectionStatusPanel.ChangeMessage(ConnectionStatus.JoinFail);

    //    StartCoroutine(connectionStatusPanel.MessageFadeOut());
    //    StartCoroutine(mainMenuPanel.TryCreateRoom());
    //}
}
