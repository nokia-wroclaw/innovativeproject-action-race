using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerTeam : MonoBehaviourPunCallbacks
{
    PhotonView pv;

    void Awake()
    {
        pv = GetComponent<PhotonView>();

        Synchronize();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (!targetPlayer.IsLocal) return;
        Debug.Log("XD");

        //object value;
        //if (changedProps.TryGetValue(PlayerProperty.Team, out value))
        //    pv.RPC("RefreshColor", RpcTarget.AllBufferedViaServer, (Team)value, pv.ViewID);
    }

    void Synchronize()
    {
        if (!pv.IsMine) return;

        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        object value;

        if (hash.TryGetValue(PlayerProperty.Team, out value))
            pv.RPC("RefreshColor", RpcTarget.AllBufferedViaServer, (Team)value, pv.ViewID);
    }

    [PunRPC]
    void RefreshColor(Team team, int viewID)
    {
        SpriteRenderer sr = PhotonNetwork.GetPhotonView(viewID).GetComponent<SpriteRenderer>();
        switch (team)
        {
            case Team.Blue:
                sr.color = Color.blue;
                break;

            case Team.Red:
                sr.color = Color.red;
                break;

            default:
                sr.color = Color.black;
                break;
        }
    }
}
