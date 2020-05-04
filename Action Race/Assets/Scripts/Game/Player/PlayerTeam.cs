using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerTeam : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    SpriteRenderer sr;

    Team team;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        sr = GetComponent<SpriteRenderer>();

        /*if (pv.IsMine)
        {
            Team team = (Team)Random.Range(1, 3);
            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add(PlayerProperty.Team, team);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }*/
        if(pv.Owner.CustomProperties.Count > 0)
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
        switch(team)
        {
            case Team.Blue:
                sr.color = new Color(0, 0, 1, 1);
                break;

            case Team.Red:
                sr.color = new Color(1, 0, 0, 1);
                break;

            default:
                sr.color = new Color(0, 1, 0, 1);
                break;
        }
    }
}
