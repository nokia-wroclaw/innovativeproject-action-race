using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CountdownTimerPanel), typeof(TimeOfDayController))]
public class CountdownTimerController : MonoBehaviourPunCallbacks
{
    CountdownTimerPanel countdownTimerPanel;
    TimeOfDayController timeOfDayController;

    bool countdown;
    double time, countdownTimerLimit;

    void Awake()
    {
        countdownTimerPanel = GetComponent<CountdownTimerPanel>();
        timeOfDayController = GetComponent<TimeOfDayController>();
    }

    void Start()
    {
        object gameStateValue;
        ExitGames.Client.Photon.Hashtable roomCustomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (roomCustomProperties.TryGetValue(RoomProperty.GameState, out gameStateValue))
        {
            object currentCountdownTimerObject, countdownTimerLimitObject;
            if (!roomCustomProperties.TryGetValue(RoomProperty.CurrentCountdownTimer, out currentCountdownTimerObject)) return;
            if (!roomCustomProperties.TryGetValue(RoomProperty.CountdownTimerLimit, out countdownTimerLimitObject)) return;
            countdownTimerLimit = (double)countdownTimerLimitObject * 60;

            switch ((State)gameStateValue)
            {
                case State.Play:
                    object startTimeValue;
                    if (!roomCustomProperties.TryGetValue(RoomProperty.StartTime, out startTimeValue)) return;

                    time = (double)currentCountdownTimerObject * 60 - (PhotonNetwork.Time - (double)startTimeValue);
                    countdownTimerPanel.CountdownTimer = time;
                    countdown = true;
                    break;

                default:
                    time = (double)currentCountdownTimerObject * 60;
                    countdownTimerPanel.CountdownTimer = time;
                    countdown = false;
                    break;
            }
        }
    }

    void Update()
    {
        if (!countdown) return;

        if (time > 0)
        {
            time -= Time.deltaTime;
            countdownTimerPanel.CountdownTimer = time;

            if (time <= countdownTimerLimit / 2 && !timeOfDayController.IsNight)
            {
                ExitGames.Client.Photon.Hashtable nightProperty = new ExitGames.Client.Photon.Hashtable();
                nightProperty.Add(RoomProperty.Night, true);
                PhotonNetwork.CurrentRoom.SetCustomProperties(nightProperty);
            }
        }
        else
        {

        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object timeLimitValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.CountdownTimerLimit, out timeLimitValue))
            countdownTimerPanel.CountdownTimer = (double)timeLimitValue * 60;

        object gameStateValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out gameStateValue))
        {
            switch ((State)gameStateValue)
            {
                case State.Play:
                    object startTimeValue, countdownTimerLimitObject;
                    ExitGames.Client.Photon.Hashtable roomCustomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
                    if (!roomCustomProperties.TryGetValue(RoomProperty.StartTime, out startTimeValue)) return;
                    if (!roomCustomProperties.TryGetValue(RoomProperty.CountdownTimerLimit, out countdownTimerLimitObject)) return;

                    countdownTimerLimit = (double)countdownTimerLimitObject * 60;
                    time = countdownTimerLimit - (PhotonNetwork.Time - (double)startTimeValue);
                    countdownTimerPanel.CountdownTimer = time;
                    countdown = true;
                    break;

                default:
                    countdown = false;
                    break;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                ExitGames.Client.Photon.Hashtable currentCountdownTimerProperty = new ExitGames.Client.Photon.Hashtable();
                currentCountdownTimerProperty.Add(RoomProperty.CurrentCountdownTimer, time / 60f);
                PhotonNetwork.CurrentRoom.SetCustomProperties(currentCountdownTimerProperty);
            }
        }
    }
}
