using UnityEngine;
using Photon.Pun;

public class GameTime : MonoBehaviourPunCallbacks
{
    GameHUDPanel gh;

    void Start()
    {
        gh = FindObjectOfType<GameHUDPanel>();
    }

    void Update()
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        double time = PhotonNetwork.Time - (double)hash[RoomProperty.StartTime];
        time = (double)hash[RoomProperty.TimeLimit] - time;

        if (time > 0)
        {
            UpdateTime(time);
        }
        else
        {
            //EndGame();
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;

        if (propertiesThatChanged.TryGetValue(RoomProperty.StartTime, out value))
            UpdateTime((double)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.TimeLimit]);
    }

    void UpdateTime(double time)
    {
        Vector2Int vTime = new Vector2Int((int)time / 60, (int)time % 60);
        gh.UpdateTimeText(vTime);
    }

    public void ResetTime()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.StartTime, PhotonNetwork.Time);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
