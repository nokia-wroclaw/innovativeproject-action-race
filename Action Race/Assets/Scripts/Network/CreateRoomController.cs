using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomController : MonoBehaviourPunCallbacks
{
    [Header("Properties")]
    [SerializeField] int roomSceneIndex;

    [Header("Custom Scripts References")]
    [SerializeField] ConnectionStatusPanel connectionStatusPanel;
    [SerializeField] CreateRoomPanel createRoomPanel;

    public void CreateGame()
    {
        StartCoroutine(connectionStatusPanel.MessageFadeIn(ConnectionStatus.Create));

        string roomName = createRoomPanel.RoomName;
        string password = createRoomPanel.Password;
        int maxPlayers = createRoomPanel.MaxPlayers;
        bool showInRoomList = createRoomPanel.ShowInRoomList;

        RoomOptions roomOps = new RoomOptions() { IsVisible = showInRoomList, IsOpen = true, MaxPlayers = (byte)maxPlayers };
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add(RoomProperty.Owner, PhotonNetwork.LocalPlayer.NickName);
        roomOps.CustomRoomProperties.Add(RoomProperty.Password, password);
        roomOps.CustomRoomProperties.Add(RoomProperty.GameState, State.Stop);
        roomOps.CustomRoomPropertiesForLobby = RoomProperty.GetPublicProperties();
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnCreatedRoom()
    {
        connectionStatusPanel.ChangeMessage(ConnectionStatus.Join);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        connectionStatusPanel.ChangeMessage(ConnectionStatus.CreateFail);
        StartCoroutine(connectionStatusPanel.MessageFadeOut());
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(roomSceneIndex);
    }
}
