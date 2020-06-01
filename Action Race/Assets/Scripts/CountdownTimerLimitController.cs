using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(CountdownTimerLimitPanel))]
public class CountdownTimerLimitController : MonoBehaviourPunCallbacks
{
    enum CountdownTimerLimit
    {
        Min_1 = 1,
        Min_3 = 3,
        Min_5 = 5,
        Min_7 = 7,
        Min_9 = 9
    }

    [Header("Properties")]
    [SerializeField] CountdownTimerLimit defaultLimitInMinutes = CountdownTimerLimit.Min_3;

    CountdownTimerLimitPanel countdownTimerLimitPanel;

    void Awake()
    {
        countdownTimerLimitPanel = GetComponent<CountdownTimerLimitPanel>();
    }

    void Start()
    {
        countdownTimerLimitPanel.ClearDropdown();

        var enumValues = System.Enum.GetValues(typeof(CountdownTimerLimit));
        foreach (var val in enumValues)
            countdownTimerLimitPanel.AddDropdownValue((int)val);

        if (PhotonNetwork.IsMasterClient)
        {
            countdownTimerLimitPanel.Value = (int)defaultLimitInMinutes;
            countdownTimerLimitPanel.ConfigureAccess(true);
        }
        else
        {
            object timeLimitValue;
            ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            if (customRoomProperties.TryGetValue(RoomProperty.CountdownTimerLimit, out timeLimitValue))
                countdownTimerLimitPanel.Value = (double)timeLimitValue;
            else
                countdownTimerLimitPanel.Value = (int)defaultLimitInMinutes;

            countdownTimerLimitPanel.ConfigureAccess(false);
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object timeLimitValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.CountdownTimerLimit, out timeLimitValue))
            countdownTimerLimitPanel.Value = (double)timeLimitValue;

        object gameStateValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out gameStateValue))
        {
            State gameState = (State)gameStateValue;
            countdownTimerLimitPanel.ConfigureAccess(PhotonNetwork.IsMasterClient, gameState);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer != newMasterClient) return;

        object gameStateValue;
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (customRoomProperties.TryGetValue(RoomProperty.GameState, out gameStateValue))
            countdownTimerLimitPanel.ConfigureAccess(true, (State)gameStateValue);
        else
            countdownTimerLimitPanel.ConfigureAccess(true, State.Stop);
    }

    public void ChangeCountdownTimerLimit(int option)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        double countdownTimerLimit = countdownTimerLimitPanel.GetCountdownTimerLimit(option);
        ExitGames.Client.Photon.Hashtable countdownTimerProperty = new ExitGames.Client.Photon.Hashtable();
        countdownTimerProperty.Add(RoomProperty.CountdownTimerLimit, countdownTimerLimit);
        countdownTimerProperty.Add(RoomProperty.CurrentCountdownTimer, countdownTimerLimit);
        PhotonNetwork.CurrentRoom.SetCustomProperties(countdownTimerProperty);
    }
}
