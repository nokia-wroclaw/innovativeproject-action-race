using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TeamPanel : MonoBehaviour
{
    [SerializeField] Team team;
    [SerializeField] RectTransform content;

    public void ChangeTeam(GameObject go)
    {
        go.transform.SetParent(content);

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(PlayerProperty.Team, team);
        go.GetComponent<NickNameTemplatePanel>().GetPlayer().SetCustomProperties(hash);
    }
}
