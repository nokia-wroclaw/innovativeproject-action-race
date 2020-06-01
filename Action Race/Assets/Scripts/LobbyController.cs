using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(LobbyPanel))]
public class LobbyController : MonoBehaviourPunCallbacks
{
    LobbyPanel lobbyPanel;

    bool isLeaving;

    void Awake()
    {
        lobbyPanel = GetComponent<LobbyPanel>();
    }

    void Start()
    {
        lobbyPanel.Players = PhotonNetwork.CurrentRoom.PlayerCount;
        lobbyPanel.MaxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;
        lobbyPanel.RoomName = PhotonNetwork.CurrentRoom.Name;

        lobbyPanel.IsActive = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            lobbyPanel.Toggle();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        lobbyPanel.Players = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        lobbyPanel.Players = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    public void LeaveRoom()
    {
        if (isLeaving) return;

        isLeaving = true;
        PhotonNetwork.LeaveRoom();
    }
}
