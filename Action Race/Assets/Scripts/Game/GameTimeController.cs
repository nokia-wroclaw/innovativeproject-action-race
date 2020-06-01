using UnityEngine;
using Photon.Pun;

public class GameTimeController : MonoBehaviourPunCallbacks
{
    [Header("Custom Scripts References")]
    [SerializeField] DayNightSystem dayNightSystem;
    [SerializeField] GameHUDPanel gameHUDPanel;
    [SerializeField] GameStateController gameStateController;

    bool countdown;

    void Start()
    {
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        UpdateTimeLimit(customRoomProperties);
        //UpdateTime(customRoomProperties);
        UpdateCountDown(customRoomProperties);
    }

    void Update()
    {
        if (!countdown) return;

        UpdateTime(PhotonNetwork.CurrentRoom.CustomProperties);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        UpdateTimeLimit(propertiesThatChanged);
        UpdateCountDown(propertiesThatChanged);
    }

    void UpdateTimeLimit(ExitGames.Client.Photon.Hashtable properties)
    {
        object value;
        if (properties.TryGetValue(RoomProperty.CountdownTimerLimit, out value))
        {
            int time = (int)value;
            Vector2Int vTime = new Vector2Int((int)time / 60, (int)time % 60);
            gameHUDPanel.UpdateTimeText(vTime);
        }
    }

    void UpdateTime(ExitGames.Client.Photon.Hashtable properties)
    {
        object value, value1;
        if (properties.TryGetValue(RoomProperty.StartTime, out value))
        {
            double time = PhotonNetwork.Time - (double)value;

            if (properties.TryGetValue(RoomProperty.CountdownTimerLimit, out value1))
            {
                int timeLimit = (int)value1;
                time = timeLimit - time;
                if (time > 0)
                {
                    Vector2Int vTime = new Vector2Int((int)time / 60, (int)time % 60);
                    gameHUDPanel.UpdateTimeText(vTime);

                    if (time <= timeLimit / 2 && !dayNightSystem.IsNight)
                    {
                        if (PhotonNetwork.IsMasterClient)
                        {
                            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                            hash.Add(RoomProperty.Night, true);
                            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
                        }
                        dayNightSystem.IsNight = true;
                        StartCoroutine(dayNightSystem.ChangeTimeOfDay());
                    }
                }
                else
                {
                    countdown = false;
                    StartCoroutine(gameStateController.EndGame());
                }
            }
        }
    }

    void UpdateCountDown(ExitGames.Client.Photon.Hashtable properties)
    {
        object value;
        if (properties.TryGetValue(RoomProperty.GameState, out value))
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
}
