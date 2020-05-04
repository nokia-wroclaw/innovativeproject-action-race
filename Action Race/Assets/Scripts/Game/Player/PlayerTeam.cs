using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerTeam : MonoBehaviourPunCallbacks
{
    PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        Synchronize();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //if (!targetPlayer.IsLocal) return;

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
                sr.color = new Color(0, 0, 1, 1);
                break;

            case Team.Red:
                sr.color = new Color(1, 0, 0, 1);
                break;
        }
    }
}
