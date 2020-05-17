using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public enum ConnectionStatusMessage
{
    CreateRoom, CreateRoomFail, JoinRandom, JoinRandomFail
}

public class ConnectionStatusController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject connectionStatusGO;
    [SerializeField] Text connectionStatusText;

    [SerializeField] string joinRandomMessage = "Searching the available room...";
    [SerializeField] string joinRandomFailedMessage = "No room available!";
    [SerializeField] string createRoomInfo = "Creating the room ...";
    [SerializeField] string createRoomFailInfo = "Unable to create the room!";
    [SerializeField] string joinRoomInfo = "Joining to the room ...";
    [SerializeField] string joinRoomFailInfo = "Unable to join the room!";

    public string ConnectionStatus
    {
        set { connectionStatusText.text = value; }
    }

    public void ShowMessage(ConnectionStatusMessage connectionStatusMessage)
    {
        string message = "";
        switch(connectionStatusMessage)
        {
            case ConnectionStatusMessage.CreateRoom:
                break;
        }

        ConnectionStatus = message;
        connectionStatusGO.SetActive(true);
    }

    public void HideMessage()
    {
        connectionStatusGO.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //csp.ShowMessage(cause.ToString());
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
