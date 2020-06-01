using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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
            Player player = p.Value;

            object teamValue;
            player.CustomProperties.TryGetValue(PlayerProperty.Team, out teamValue);
            teamPanel.AddPlayer(player.ActorNumber, player.NickName, player.IsLocal, player.IsMasterClient, teamValue != null ? (Team)teamValue : Team.None);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        teamPanel.AddPlayer(newPlayer.ActorNumber, newPlayer.NickName, newPlayer.IsLocal, newPlayer.IsMasterClient, Team.None);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        teamPanel.RemovePlayer(otherPlayer.ActorNumber);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        object teamValue;
        if (changedProps.TryGetValue(PlayerProperty.Team, out teamValue))
            teamPanel.ChangePlayerTeam(targetPlayer.ActorNumber, (Team)teamValue);
    }
}
