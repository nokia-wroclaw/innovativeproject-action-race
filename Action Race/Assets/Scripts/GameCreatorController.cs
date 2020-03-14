using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameCreatorController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameCreatorPanel gameCreatorPanel;
    [SerializeField] int roomSceneIndex;

    public void CreateGame()
    {
        string serverName = gameCreatorPanel.GetServerName();

        RoomOptions roomOps = new RoomOptions();
        roomOps.IsVisible = true;
        roomOps.IsOpen = true;
        roomOps.MaxPlayers = 10;

        PhotonNetwork.CreateRoom(serverName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        PhotonNetwork.LoadLevel(roomSceneIndex);
    }
}
