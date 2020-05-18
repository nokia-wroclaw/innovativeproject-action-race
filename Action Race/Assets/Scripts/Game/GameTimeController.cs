using UnityEngine;
using Photon.Pun;

public class GameTimeController : MonoBehaviourPunCallbacks
{
    [Header("Custom Scripts References")]
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
        if (properties.TryGetValue(RoomProperty.TimeLimit, out value))
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

            if (properties.TryGetValue(RoomProperty.TimeLimit, out value1))
            {
                time = (int)value1 - time;
                if (time > 0)
                {
                    Vector2Int vTime = new Vector2Int((int)time / 60, (int)time % 60);
                    gameHUDPanel.UpdateTimeText(vTime);
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
