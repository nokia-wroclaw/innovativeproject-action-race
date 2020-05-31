using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CountdownTimerPanel))]
public class CountdownTimerController : MonoBehaviourPunCallbacks
{
    CountdownTimerPanel countdownTimerPanel;

    bool countdown;
    double time;

    void Awake()
    {
        countdownTimerPanel = GetComponent<CountdownTimerPanel>();
    }

    void Start()
    {
        object startTimeValue, gameStateValue;
        ExitGames.Client.Photon.Hashtable roomCustomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (roomCustomProperties.TryGetValue(RoomProperty.StartTime, out startTimeValue) &&
            roomCustomProperties.TryGetValue(RoomProperty.GameState, out gameStateValue))
        {
            switch((State)gameStateValue)
            {
                case State.Play:
                    time = PhotonNetwork.Time - (double)startTimeValue;
                    countdownTimerPanel.CountdownTimer = time;
                    countdown = true;
                    break;
            }
        }
    }

    void Update()
    {
        if (!countdown) return;

        time -= Time.deltaTime;
        countdownTimerPanel.CountdownTimer = time;
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object timeLimitValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.TimeLimit, out timeLimitValue))
            countdownTimerPanel.CountdownTimer = (int)timeLimitValue * 60;
    }
}
