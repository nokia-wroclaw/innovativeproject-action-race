using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionStatusController : MonoBehaviourPunCallbacks
{
    ConnectionStatusPanel csp;

    void Awake()
    {
        csp = FindObjectOfType<ConnectionStatusPanel>();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        csp.ShowMessage(cause.ToString());
    }

    //public override void on
    //public void ShowConnectionStatus()
    //{
    //    connectionStatusPanel.SetActive(true);
    //}

    //public IEnumerator HideConnectionStatus()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    connectionStatusPanel.SetActive(false);
    //}

    //public void SetJoinRandomFailedMessage()
    //{
    //    connectionStatusPanel.Message = joinRandomFailedMessage;
    //}
    //public void SetJoinRandomMessage()
    //{
    //    connectionStatusPanel.Message = joinRandomMessage;
    //}

    //public void SetCreateRoomMessage()
    //{
    //    connectionStatusPanel.Message = createRoomMessage;
    //}

    //public void SetJoinRoomMessage()
    //{
    //    connectionStatusPanel.Message = joinRoomMessage;
    //}

    //public void SetCreateRoomFailedMessage()
    //{
    //    connectionStatusPanel.Message = createRoomFailedMessage;
    //}
}
