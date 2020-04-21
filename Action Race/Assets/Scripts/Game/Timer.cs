using UnityEngine;
using Photon.Pun;

public class Timer : MonoBehaviour
{
    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();

        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.FetchServerTimestamp();
            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add(RoomProperty.StartTime, PhotonNetwork.Time);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }
        UpdateTime((double)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.GameTime]);
    }

    void Update()
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        double time = PhotonNetwork.Time - (double)hash[RoomProperty.StartTime];
        time = (double)hash[RoomProperty.GameTime] - time;
        UpdateTime(time);

        Debug.Log(PhotonNetwork.Time + " " + (double)hash[RoomProperty.StartTime]);
    }

    void UpdateTime(double time)
    {
        Vector2Int gameTime = new Vector2Int((int)time / 60, (int)time % 60);
        scoreBoard.SetTime(gameTime);
    }
}
