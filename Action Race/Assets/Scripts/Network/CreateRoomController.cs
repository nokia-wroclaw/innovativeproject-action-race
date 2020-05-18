using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] int roomSceneIndex;

    ConnectionStatusPanel csp;
    CreateRoomPanel crp;

    void Awake()
    {
        csp = FindObjectOfType<ConnectionStatusPanel>();
        crp = FindObjectOfType<CreateRoomPanel>();
    }

    public void CreateGame()
    {
        StartCoroutine(csp.MessageFadeIn(ConnectionStatus.Create));

        string roomName = crp.RoomName;
        string password = crp.Password;
        int maxPlayers = crp.MaxPlayers;
        bool showInRoomList = crp.ShowInRoomList;

        RoomOptions roomOps = new RoomOptions() { IsVisible = showInRoomList, IsOpen = true, MaxPlayers = (byte)maxPlayers };
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add(RoomProperty.Owner, PhotonNetwork.LocalPlayer.NickName);
        roomOps.CustomRoomProperties.Add(RoomProperty.Password, password);
        roomOps.CustomRoomPropertiesForLobby = RoomProperty.GetPublicProperties();
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnCreatedRoom()
    {
        csp.ChangeMessage(ConnectionStatus.Join);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        csp.ChangeMessage(ConnectionStatus.CreateFail);
        StartCoroutine(csp.MessageFadeOut());
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(roomSceneIndex);
    }
}
