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
            Photon.Realtime.Player player = p.Value;

            object teamValue;
            player.CustomProperties.TryGetValue(PlayerProperty.Team, out teamValue);
            teamPanel.AddPlayer(player.ActorNumber, player.NickName, player.IsLocal, player.IsMasterClient, teamValue != null ? (Team)teamValue : Team.None);
        }
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
    }
}
