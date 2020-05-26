using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameLobbyController : MonoBehaviourPunCallbacks
{
    [Header("Custom Scripts References")]
    [SerializeField] GameLobbyPanel gameLobbyPanel;

    bool isLeaving;

    void Start()
    {
        gameLobbyPanel.RoomName = PhotonNetwork.CurrentRoom.Name;
        gameLobbyPanel.Players = PhotonNetwork.CurrentRoom.PlayerCount;
        gameLobbyPanel.MaxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;

        ResetTimeLimit();
        ResetScoreLimit();

        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        UpdateTimeLimit(customRoomProperties);
        UpdateScoreLimit(customRoomProperties);

        UpdateTeams();

        if (PhotonNetwork.IsMasterClient)
            gameLobbyPanel.ConfigureMasterClientPanel(State.Stop);
        else
            gameLobbyPanel.ConfigureClientPanel();

        gameLobbyPanel.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            gameLobbyPanel.gameObject.SetActive(!gameLobbyPanel.gameObject.activeInHierarchy);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        UpdateTimeLimit(propertiesThatChanged);
        UpdateScoreLimit(propertiesThatChanged);
        UpdatePanelLayout(propertiesThatChanged);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        object value;
        if (changedProps.TryGetValue(PlayerProperty.Team, out value))
            gameLobbyPanel.ChangePlayerTeam(targetPlayer.ActorNumber, (Team)value);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        gameLobbyPanel.ChangePlayerIsMasterClient(newMasterClient.ActorNumber);

        if (PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            object value;
            if(customRoomProperties.TryGetValue(RoomProperty.GameState, out value))
                gameLobbyPanel.ConfigureMasterClientPanel((State)value);
            else
                gameLobbyPanel.ConfigureMasterClientPanel(State.Stop);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        gameLobbyPanel.Players = PhotonNetwork.CurrentRoom.PlayerCount;
        gameLobbyPanel.AddPlayer(newPlayer.ActorNumber, newPlayer.NickName, newPlayer.IsLocal, newPlayer.IsMasterClient, Team.None);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        gameLobbyPanel.Players = PhotonNetwork.CurrentRoom.PlayerCount;
        gameLobbyPanel.RemovePlayer(otherPlayer.ActorNumber);
    }

    public override void OnLeftRoom()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(PlayerProperty.Team, Team.None);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    void UpdateTimeLimit(ExitGames.Client.Photon.Hashtable properties)
    {
        object value;
        if (properties.TryGetValue(RoomProperty.TimeLimit, out value))
            gameLobbyPanel.TimeLimit = (int)value / 60;
    }

    void UpdateScoreLimit(ExitGames.Client.Photon.Hashtable properties)
    {
        object value;
        if (properties.TryGetValue(RoomProperty.ScoreLimit, out value))
            gameLobbyPanel.ScoreLimit = (int)value;
    }

    void UpdatePanelLayout(ExitGames.Client.Photon.Hashtable properties)
    {
        object value;
        if (properties.TryGetValue(RoomProperty.GameState, out value))
            if(PhotonNetwork.IsMasterClient)
                gameLobbyPanel.ConfigureMasterClientPanel((State)value);
    }

    void ResetTimeLimit()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.TimeLimit, gameLobbyPanel.DefaultTimeLimit * 60);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    void ResetScoreLimit()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.ScoreLimit, gameLobbyPanel.DefaultScoreLimit);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    void UpdateTeams()
    {
        foreach (var p in PhotonNetwork.CurrentRoom.Players)
        {
            Player player = p.Value;

            object value;
            player.CustomProperties.TryGetValue(PlayerProperty.Team, out value);
            gameLobbyPanel.AddPlayer(player.ActorNumber, player.NickName, player.IsLocal, player.IsMasterClient, value != null ? (Team)value : Team.None);
        }
    }

    public void ChangeTimeLimit()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.TimeLimit, gameLobbyPanel.TimeLimit * 60);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void ChangeScoreLimit()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.ScoreLimit, gameLobbyPanel.ScoreLimit);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void ChangePlayerTeam(int actorNumber, Team team)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(PlayerProperty.Team, team);
        PhotonNetwork.CurrentRoom.GetPlayer(actorNumber).SetCustomProperties(hash);
    }

    public void LeaveRoom()
    {
        if (isLeaving) return;

        isLeaving = true;
        PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.GameState, State.Play);
        hash.Add(RoomProperty.StartTime, PhotonNetwork.Time);
        hash.Add(RoomProperty.Night, false);
        hash.Add(RoomProperty.BlueScore, 0);
        hash.Add(RoomProperty.RedScore, 0);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void StopGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.GameState, State.Stop);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
