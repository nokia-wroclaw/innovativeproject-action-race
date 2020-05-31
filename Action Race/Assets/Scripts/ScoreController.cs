using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(ScorePanel))]
public class ScoreController : MonoBehaviourPunCallbacks
{
    ScorePanel scorePanel;

    void Awake()
    {
        scorePanel = GetComponent<ScorePanel>();
    }

    void Start()
    {
        object blueScoreValue, redScoreValue;
        ExitGames.Client.Photon.Hashtable roomCustomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (roomCustomProperties.TryGetValue(RoomProperty.BlueScore, out blueScoreValue))
            scorePanel.BlueScore = (int)blueScoreValue;
        else
            scorePanel.BlueScore = 0;

        if (roomCustomProperties.TryGetValue(RoomProperty.RedScore, out redScoreValue))
            scorePanel.RedScore = (int)redScoreValue;
        else
            scorePanel.RedScore = 0;
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object blueScoreValue, redScoreValue;

        if (propertiesThatChanged.TryGetValue(RoomProperty.BlueScore, out blueScoreValue))
            scorePanel.BlueScore = (int)blueScoreValue;

        if (propertiesThatChanged.TryGetValue(RoomProperty.RedScore, out redScoreValue))
            scorePanel.RedScore = (int)redScoreValue;
    }
}
