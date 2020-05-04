using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class TeamPanelController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerTemplate;
    [SerializeField] TeamPanel blueTeamPanel;
    [SerializeField] TeamPanel noTeamPanel;
    [SerializeField] TeamPanel redTeamPanel;

    Dictionary<Player, GameObject> playersTemplates = new Dictionary<Player, GameObject>();

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        object value;
        if (changedProps.TryGetValue(PlayerProperty.Team, out value))
            ChangeTeam(targetPlayer, (Team)value);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(PlayerProperty.Team, Team.None);
        newPlayer.SetCustomProperties(hash);
    }

    public void ChangeTeam(Player player, Team team)
    {
        GameObject go;
        if(playersTemplates.TryGetValue(player, out go))
        {

        }
        else
        {
            go = Instantiate(playerTemplate, noTeamPanel.transform);
            go.GetComponent<NickNameTemplatePanel>().SetUpTemplate(player);
        }
    }
}
