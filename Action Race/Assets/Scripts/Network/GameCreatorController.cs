using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameCreatorController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameCreatorPanel gameCreatorPanel;
    [SerializeField] int roomSceneIndex;

    public void CreateGame()
    {
        string roomName = gameCreatorPanel.GetRoomName();
        string password = gameCreatorPanel.GetPassword();
        int maxPlayers = gameCreatorPanel.GetMaxPlayers();
        bool isVisibleInLobby = gameCreatorPanel.IsVisibleInLobby();

        RoomOptions roomOps = new RoomOptions() { IsVisible = isVisibleInLobby, IsOpen = true, MaxPlayers = (byte)maxPlayers };
        PhotonNetwork.CreateRoom(roomName, roomOps);
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
