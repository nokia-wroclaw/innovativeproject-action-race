using UnityEngine;
using Photon.Pun;

public class ConnectionStatusController : MonoBehaviourPunCallbacks
{
    [SerializeField] ConnectionStatusPanel connectionStatusPanel;

    [SerializeField] string createRoomMessage = "Creating the room ...";
    [SerializeField] string createRoomFailedMessage = "Create Room Fail";
    [SerializeField] string joinRoomMessage = "Joining to the room ...";

    public void SetCreateRoomMessage()
    {
        connectionStatusPanel.Message = createRoomMessage;
    }

    public void SetJoinRoomMessage()
    {
        connectionStatusPanel.Message = joinRoomMessage;
    }

    public void SetCreateRoomFailedMessage()
    {
        connectionStatusPanel.Message = createRoomFailedMessage;
    }
}
