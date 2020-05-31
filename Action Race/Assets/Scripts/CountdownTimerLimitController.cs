using UnityEngine;
using Photon.Pun;

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
            countdownTimerLimitPanel.Value = (int)defaultLimitInMinutes;
        else
        {
            object timeLimitValue;
            ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            if (customRoomProperties.TryGetValue(RoomProperty.TimeLimit, out timeLimitValue))
                countdownTimerLimitPanel.Value = (int)timeLimitValue;
            else
                countdownTimerLimitPanel.Value = (int)defaultLimitInMinutes;
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object timeLimitValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.TimeLimit, out timeLimitValue))
            countdownTimerLimitPanel.Value = (int)timeLimitValue;
    }

    public void ChangeCountdownTimerLimit(int option)
    {
        int countdownTimerLimit = countdownTimerLimitPanel.GetCountdownTimerLimit(option);

        ExitGames.Client.Photon.Hashtable countdownTimerProperty = new ExitGames.Client.Photon.Hashtable();
        countdownTimerProperty.Add(RoomProperty.TimeLimit, countdownTimerLimit);
        PhotonNetwork.CurrentRoom.SetCustomProperties(countdownTimerProperty);
    }
}
