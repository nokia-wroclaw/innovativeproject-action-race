using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameLobbyController : MonoBehaviourPunCallbacks
{
    public void ChangeTimeLimit(int time)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.TimeLimit, time);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void ChangeScoreLimit(int score)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.ScoreLimit, score);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
