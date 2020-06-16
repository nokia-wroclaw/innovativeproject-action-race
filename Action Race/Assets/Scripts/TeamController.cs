using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

[RequireComponent(typeof(TeamPanel))]
public class TeamController : MonoBehaviourPunCallbacks
{
    TeamPanel teamPanel;

    void Awake()
    {
        teamPanel = GetComponent<TeamPanel>();
    }

    void Start()
    {
        foreach (var p in PhotonNetwork.CurrentRoom.Players)
        {
            Photon.Realtime.Player player = p.Value;

            object teamValue;
            player.CustomProperties.TryGetValue(PlayerProperty.Team, out teamValue);
            teamPanel.AddPlayer(player.ActorNumber, player.NickName, player.IsLocal, player.IsMasterClient, teamValue != null ? (Team)teamValue : Team.None);
        }

        teamPanel.ConfigureAccess(PhotonNetwork.IsMasterClient);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object gameStateValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out gameStateValue))
            teamPanel.ConfigureAccess(PhotonNetwork.IsMasterClient, (State)gameStateValue);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        object teamValue;
        newPlayer.CustomProperties.TryGetValue(PlayerProperty.Team, out teamValue);
        teamPanel.AddPlayer(newPlayer.ActorNumber, newPlayer.NickName, newPlayer.IsLocal, newPlayer.IsMasterClient, teamValue != null ? (Team)teamValue : Team.None);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        teamPanel.RemovePlayer(otherPlayer.ActorNumber);
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        object teamValue;
        if (changedProps.TryGetValue(PlayerProperty.Team, out teamValue))
            teamPanel.ChangePlayerTeam(targetPlayer.ActorNumber, (Team)teamValue);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        teamPanel.UpdateNewMasterClient(newMasterClient.ActorNumber);

        ExitGames.Client.Photon.Hashtable ownerProperty = new ExitGames.Client.Photon.Hashtable();
        ownerProperty.Add(RoomProperty.Owner, newMasterClient.NickName);
        PhotonNetwork.CurrentRoom.SetCustomProperties(ownerProperty);


        if (PhotonNetwork.LocalPlayer != newMasterClient)
            return;

        object gameStateValue;
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (customRoomProperties.TryGetValue(RoomProperty.GameState, out gameStateValue))
            teamPanel.ConfigureAccess(true, (State)gameStateValue);
        else
            teamPanel.ConfigureAccess(true, State.Stop);
    }

    public void MoveTeamToSpect(int team)
    {
        switch ((Team)team)
        {
            case Team.Blue:
                foreach (RectTransform child in teamPanel.BlueTeamPanel)
                    child.GetComponent<PlayerTemplateController>().ChangePlayerTeam(Team.None);
                break;

            case Team.Red:
                foreach (RectTransform child in teamPanel.RedTeamPanel)
                    child.GetComponent<PlayerTemplateController>().ChangePlayerTeam(Team.None);
                break;
        }
    }

    public void SwapTeams()
    {
        foreach (RectTransform child in teamPanel.BlueTeamPanel)
            child.GetComponent<PlayerTemplateController>().ChangePlayerTeam(Team.Red);

        foreach (RectTransform child in teamPanel.RedTeamPanel)
            child.GetComponent<PlayerTemplateController>().ChangePlayerTeam(Team.Blue);
    }

    public void RandTeams()
    {
        Dictionary<int, GameObject> playersTemplates = teamPanel.GetPlayersTemplates();
        int blueCount = playersTemplates.Count / 2, redCount = playersTemplates.Count - blueCount;
        foreach(var playerTemplate in playersTemplates)
        {
            Team team;
            if(blueCount > 0)
            {
                if (redCount > 0)
                {
                    int r = Random.Range(0, 2);
                    if(r == 0)
                    {
                        team = Team.Blue;
                        blueCount--;
                    }
                    else
                    {
                        team = Team.Red;
                        redCount--;
                    }
                }
                else
                {
                    team = Team.Blue;
                    blueCount--;
                }
            }
            else
            {
                if (redCount > 0)
                {
                    team = Team.Red;
                    redCount--;
                }
                else
                    return;
            }
            playerTemplate.Value.GetComponent<PlayerTemplateController>().ChangePlayerTeam(team);
        }
    }
}
