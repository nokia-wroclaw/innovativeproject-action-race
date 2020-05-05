using UnityEngine;
using Photon.Pun;

public class GameTimeController : MonoBehaviourPunCallbacks
{
    GameHUDPanel ghp;
    GameStateController gsc;
    bool countdown;

    void Start()
    {
        ghp = FindObjectOfType<GameHUDPanel>();
        gsc = FindObjectOfType<GameStateController>();

        Synchronize();
    }

    void Update()
    {
        if (!countdown) return;

        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        object value;
        if (hash.TryGetValue(RoomProperty.StartTime, out value))
        {
            double time = PhotonNetwork.Time - (double)value;
            time = (int)hash[RoomProperty.TimeLimit] - time;

            if (time > 0)
            {
                UpdateTime(time);
            }
            else
            {
                countdown = false;
                StartCoroutine(gsc.EndGame());
            }
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;

        if (propertiesThatChanged.TryGetValue(RoomProperty.TimeLimit, out value))
            UpdateTime((int)value);

        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
        {
            ResetTime();
            switch ((State)value)
            {
                case State.Play:
                    countdown = true;
                    break;

                default:
                    countdown = false;
                    break;
            }
        }
    }

    void Synchronize()
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        object value;

        if (hash.TryGetValue(RoomProperty.TimeLimit, out value))
            UpdateTime((int)value);

        if (hash.TryGetValue(RoomProperty.GameState, out value))
        {
            switch ((State)value)
            {
                case State.Play:
                    countdown = true;
                    break;

                default:
                    countdown = false;
                    break;
            }
        }
    }

    void UpdateTime(double time)
    {
        Vector2Int vTime = new Vector2Int((int)time / 60, (int)time % 60);
        ghp.UpdateTimeText(vTime);
    }

    public void ResetTime()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.StartTime, PhotonNetwork.Time);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
