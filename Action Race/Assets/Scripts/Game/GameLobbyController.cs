using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameLobbyController : MonoBehaviourPunCallbacks
{
    GameLobbyPanel glp;

    bool isLeaving;

    void Awake()
    {
        glp = FindObjectOfType<GameLobbyPanel>();
    }

    void Start()
    {
        glp.RoomName = PhotonNetwork.CurrentRoom.Name;
        glp.Players = PhotonNetwork.CurrentRoom.PlayerCount;
        glp.MaxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;

        ResetTimeLimit();
        ResetScoreLimit();

        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        UpdateTimeLimit(customRoomProperties);
        UpdateScoreLimit(customRoomProperties);

        UpdateTeams();

        if (PhotonNetwork.IsMasterClient)
            glp.ConfigureMasterClientPanel(State.Stop);
        else
            glp.ConfigureClientPanel();

        glp.SetActive(true);
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
            glp.ChangePlayerTeam(targetPlayer.ActorNumber, (Team)value);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        glp.ChangePlayerIsMasterClient(newMasterClient.ActorNumber);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        glp.Players = PhotonNetwork.CurrentRoom.PlayerCount;
        glp.AddPlayer(newPlayer.ActorNumber, newPlayer.NickName, newPlayer.IsLocal, newPlayer.IsMasterClient, Team.None);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        glp.Players = PhotonNetwork.CurrentRoom.PlayerCount;
        glp.RemovePlayer(otherPlayer.ActorNumber);
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
            glp.TimeLimit = (int)value / 60;
    }

    void UpdateScoreLimit(ExitGames.Client.Photon.Hashtable properties)
    {
        object value;
        if (properties.TryGetValue(RoomProperty.ScoreLimit, out value))
            glp.ScoreLimit = (int)value;
    }

    void UpdatePanelLayout(ExitGames.Client.Photon.Hashtable properties)
    {
        object value;
        if (properties.TryGetValue(RoomProperty.GameState, out value))
            if(PhotonNetwork.IsMasterClient)
                glp.ConfigureMasterClientPanel((State)value);
    }

    void ResetTimeLimit()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.TimeLimit, glp.DefaultTimeLimit * 60);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    void ResetScoreLimit()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.ScoreLimit, glp.DefaultScoreLimit);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    void UpdateTeams()
    {
        foreach (var p in PhotonNetwork.CurrentRoom.Players)
        {
            Player player = p.Value;

            object value;
            player.CustomProperties.TryGetValue(PlayerProperty.Team, out value);
            glp.AddPlayer(player.ActorNumber, player.NickName, player.IsLocal, player.IsMasterClient, value != null ? (Team)value : Team.None);
        }
    }

    public void ChangeTimeLimit()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.TimeLimit, glp.TimeLimit * 60);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void ChangeScoreLimit()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.ScoreLimit, glp.ScoreLimit);
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
