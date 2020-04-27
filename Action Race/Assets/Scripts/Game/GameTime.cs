using UnityEngine;
using Photon.Pun;

public class GameTime : MonoBehaviour
{
    GameHUDPanel gh;

    void Start()
    {
        gh = FindObjectOfType<GameHUDPanel>();

        if (PhotonNetwork.IsMasterClient)
        {
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

        if (time > 0)
        {
            UpdateTime(time);
        }
        /*else
        {
            EndGame();
            Debug.Log("END");
        }*/
    }

    void UpdateTime(double time)
    {
        Vector2Int vTime = new Vector2Int((int)time / 60, (int)time % 60);
        gh.UpdateTimeText(vTime);
    }
}
