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

    public void AddScore(Team team, int score)
    {
        object scoreValue;
        ExitGames.Client.Photon.Hashtable scoreProperty = new ExitGames.Client.Photon.Hashtable();

        switch (team)
        {
            case Team.Blue:
                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(RoomProperty.BlueScore, out scoreValue))
                    score += (int)scoreValue;

                scoreProperty.Add(RoomProperty.BlueScore, score);
                break;

            case Team.Red:
                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(RoomProperty.RedScore, out scoreValue))
                    score += (int)scoreValue;

                scoreProperty.Add(RoomProperty.RedScore, score);
                break;
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(scoreProperty);
    }
}
