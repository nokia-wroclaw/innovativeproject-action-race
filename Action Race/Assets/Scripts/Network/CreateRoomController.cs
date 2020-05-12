using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] int roomSceneIndex;
    [SerializeField] NicknameManager nicknameManager;

    CreateRoomPanel createRoomPanel;

    void Awake()
    {
        createRoomPanel = FindObjectOfType<CreateRoomPanel>();
    }

    public void CreateGame()
    {
        string roomName = createRoomPanel.RoomName;
        string password = createRoomPanel.Password;
        int maxPlayers = createRoomPanel.MaxPlayers;
        bool showInRoomList = createRoomPanel.ShowInRoomList;

        RoomOptions roomOps = new RoomOptions() { IsVisible = showInRoomList, IsOpen = true, MaxPlayers = (byte)maxPlayers };
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add(RoomProperty.Owner, nicknameManager.NickName);
        roomOps.CustomRoomProperties.Add(RoomProperty.Password, password);
        roomOps.CustomRoomPropertiesForLobby = RoomProperty.GetPublicProperties();
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(roomSceneIndex);
    }
}
