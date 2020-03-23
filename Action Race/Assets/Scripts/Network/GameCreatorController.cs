using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameCreatorController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameCreatorPanel gameCreatorPanel;
    [SerializeField] int roomSceneIndex;
    [SerializeField] NicknameController nicknameController;

    public void CreateGame()
    {
        string roomName = gameCreatorPanel.RoomName;
        string password = gameCreatorPanel.GetPassword();
        int maxPlayers = gameCreatorPanel.MaxPlayers;
        bool isVisibleInLobby = gameCreatorPanel.IsVisibleInLobby();

        RoomOptions roomOps = new RoomOptions() { IsVisible = isVisibleInLobby, IsOpen = true, MaxPlayers = (byte)maxPlayers };
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add(RoomProperty.Owner, nicknameController.GetNickname());
        roomOps.CustomRoomProperties.Add(RoomProperty.Password, password);
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
