using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(ScoreLimitPanel))]
public class ScoreLimitController : MonoBehaviourPunCallbacks
{
    enum ScoreLimit
    {
        Point_1 = 1,
        Point_3 = 3,
        Point_5 = 5,
        Point_7 = 7,
        Point_9 = 9
    }

    [Header("Properties")]
    [SerializeField] ScoreLimit defaultScoreLimit = ScoreLimit.Point_3;

    ScoreLimitPanel scoreLimitPanel;

    void Awake()
    {
        scoreLimitPanel = GetComponent<ScoreLimitPanel>();
    }

    void Start()
    {
        scoreLimitPanel.ClearDropdown();

        var enumValues = System.Enum.GetValues(typeof(ScoreLimit));
        foreach (var val in enumValues)
            scoreLimitPanel.AddDropdownValue((int)val);

        if (PhotonNetwork.IsMasterClient)
            scoreLimitPanel.Value = (int)defaultScoreLimit;
        else
        {
            object scoreLimitValue;
            ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            if (customRoomProperties.TryGetValue(RoomProperty.ScoreLimit, out scoreLimitValue))
                scoreLimitPanel.Value = (int)scoreLimitValue;
            else
                scoreLimitPanel.Value = (int)defaultScoreLimit;
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object scoreLimitValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.ScoreLimit, out scoreLimitValue))
            scoreLimitPanel.Value = (int)scoreLimitValue;
    }

    public void ChangeScoreLimit(int option)
    {
        int scoreLimit = scoreLimitPanel.GetScoreLimit(option);

        ExitGames.Client.Photon.Hashtable countdownTimerProperty = new ExitGames.Client.Photon.Hashtable();
        countdownTimerProperty.Add(RoomProperty.ScoreLimit, scoreLimit);
        PhotonNetwork.CurrentRoom.SetCustomProperties(countdownTimerProperty);
    }
}
