using UnityEngine;
using Photon.Pun;

public class PlayerTeam : MonoBehaviour
{
    SpriteRenderer sr;
    PhotonView pv;

    Team team;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        pv = GetComponent<PhotonView>();

        if (pv.IsMine)
        {
            Team team = (Team)Random.Range(1, 3);
            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add(PlayerProperty.Team, team);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
        SetTeam((Team)pv.Owner.CustomProperties[PlayerProperty.Team]);
    }

    public void SetTeam(Team team)
    {
        this.team = team;
        RefreshColor();
    }

    public Team GetTeam()
    {
        return team;
    }

    void RefreshColor()
    {
        if (team == Team.Red)
            sr.color = new Color(1, 0, 0, 1);
        if (team == Team.Blue)
            sr.color = new Color(0, 0, 1, 1);
    }
}
