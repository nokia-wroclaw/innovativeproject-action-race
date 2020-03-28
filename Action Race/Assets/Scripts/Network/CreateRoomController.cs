using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] CreateRoomPanel createRoomPanel;
    [SerializeField] int roomSceneIndex;

    [SerializeField] NicknameController nicknameController;
    [SerializeField] ConnectionStatusController connectionStatusController;

    public void CreateGame()
    {
        string roomName = createRoomPanel.RoomName;
        string roomPassword = createRoomPanel.RoomPassword;
        int roomMaxPlayers = createRoomPanel.RoomMaxPlayers;
        bool roomIsVisible = createRoomPanel.IsVisible;

        RoomOptions roomOps = new RoomOptions() { IsVisible = roomIsVisible, IsOpen = true, MaxPlayers = (byte)roomMaxPlayers };
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add(RoomProperty.Owner, nicknameController.GetNickname());
        roomOps.CustomRoomProperties.Add(RoomProperty.Password, roomPassword);
        roomOps.CustomRoomPropertiesForLobby = RoomProperty.GetProperties();
        PhotonNetwork.CreateRoom(roomName, roomOps);

        connectionStatusController.SetCreateRoomMessage();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
        connectionStatusController.SetCreateRoomFailedMessage();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        connectionStatusController.SetJoinRoomMessage();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        PhotonNetwork.LoadLevel(roomSceneIndex);
    }
}
