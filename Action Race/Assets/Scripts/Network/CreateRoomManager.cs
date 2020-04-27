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

        PhotonNetwork.FetchServerTimestamp();

        RoomOptions roomOps = new RoomOptions() { IsVisible = roomIsVisible, IsOpen = true, MaxPlayers = (byte)roomMaxPlayers };
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add(RoomProperty.Owner, nicknameManager.NickName);
        roomOps.CustomRoomProperties.Add(RoomProperty.Password, roomPassword);
        roomOps.CustomRoomProperties.Add(RoomProperty.RedScore, 0);
        roomOps.CustomRoomProperties.Add(RoomProperty.BlueScore, 0);
        roomOps.CustomRoomProperties.Add(RoomProperty.StartTime, PhotonNetwork.Time);
        roomOps.CustomRoomProperties.Add(RoomProperty.GameTime, 60.0);
        roomOps.CustomRoomPropertiesForLobby = RoomProperty.GetPublicProperties();
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
