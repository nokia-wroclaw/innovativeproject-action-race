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
