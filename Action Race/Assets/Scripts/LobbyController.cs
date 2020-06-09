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

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object gameStateValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out gameStateValue))
        {
            switch ((State)gameStateValue)
            {
                case State.Stop:
                    lobbyPanel.IsActive = true;
                    break;

                case State.Play:
                    object teamValue;
                    ExitGames.Client.Photon.Hashtable customPlayerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
                    customPlayerProperties.TryGetValue(PlayerProperty.Team, out teamValue);

                    if(teamValue != null ? (Team)teamValue != Team.None : false)
                        lobbyPanel.IsActive = false;
                    break;
            }
        }
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (!targetPlayer.IsLocal) return;

        object teamValue;
        if (changedProps.TryGetValue(PlayerProperty.Team, out teamValue))
        {
            if ((Team)teamValue == Team.None)
                return;

            object gameStateValue;
            ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            if (customRoomProperties.TryGetValue(RoomProperty.GameState, out gameStateValue))
            {
                switch((State)gameStateValue)
                {
                    case State.Play:
                        lobbyPanel.IsActive = false;
                        break;

                    default:
                        lobbyPanel.IsActive = true;
                        break;
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        lobbyPanel.Players = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        lobbyPanel.Players = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);

        ExitGames.Client.Photon.Hashtable teamProperty = new ExitGames.Client.Photon.Hashtable();
        teamProperty.Add(PlayerProperty.Team, Team.None);
        PhotonNetwork.LocalPlayer.SetCustomProperties(teamProperty);
    }

    public void LeaveRoom()
    {
        if (isLeaving) return;

        isLeaving = true;
        PhotonNetwork.LeaveRoom();
    }
}
