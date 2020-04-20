using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] CreateRoomInfo createRoomInfo;
    [SerializeField] int roomSceneIndex;
    [SerializeField] NicknameManager nicknameManager;

    public void CreateGame()
    {
        string roomName = createRoomInfo.RoomName;
        string roomPassword = createRoomInfo.RoomPassword;
        int roomMaxPlayers = createRoomInfo.RoomMaxPlayers;
        bool roomIsVisible = createRoomInfo.IsVisible;

        RoomOptions roomOps = new RoomOptions() { IsVisible = roomIsVisible, IsOpen = true, MaxPlayers = (byte)roomMaxPlayers };
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add(RoomProperty.Owner, nicknameManager.NickName);
        roomOps.CustomRoomProperties.Add(RoomProperty.Password, roomPassword);
        roomOps.CustomRoomPropertiesForLobby = RoomProperty.GetProperties();
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
