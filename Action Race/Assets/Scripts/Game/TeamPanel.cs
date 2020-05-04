using UnityEngine;

public class TeamPanel : MonoBehaviour
{
    [SerializeField] Team team;
    [SerializeField] RectTransform content;

    public void AddToPanel(GameObject go)
    {
        go.transform.SetParent(content);
        ChangeTeam(go);
    }

    void ChangeTeam(GameObject go)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(PlayerProperty.Team, team);
        go.GetComponent<PlayerTemplate>().GetPlayer().SetCustomProperties(hash);
    }
}
