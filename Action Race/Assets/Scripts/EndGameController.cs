using UnityEngine;
using Photon.Pun;
using System.Collections;

[RequireComponent(typeof(EndGamePanel))]
public class EndGameController : MonoBehaviourPunCallbacks
{
    EndGamePanel endGamePanel;

    State state;

    void Awake()
    {
        endGamePanel = GetComponent<EndGamePanel>();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object gameStateValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out gameStateValue))
        {
            if ((State)gameStateValue != state && (State)gameStateValue == State.End)
                StartCoroutine(EndGame());

            state = (State)gameStateValue;
        }
    }

    IEnumerator EndGame()
    {
        object blueScoresValue, redScoresValue;
        ExitGames.Client.Photon.Hashtable roomCustomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        int blueScores, redScores;

        if (roomCustomProperties.TryGetValue(RoomProperty.BlueScore, out blueScoresValue))
            blueScores = (int)blueScoresValue;
        else
            blueScores = 0;

        if (roomCustomProperties.TryGetValue(RoomProperty.RedScore, out redScoresValue))
            redScores = (int)redScoresValue;
        else
            redScores = 0;

        Team winnerTeam;
        if (redScores > blueScores) winnerTeam = Team.Red;
        else if (blueScores > redScores) winnerTeam = Team.Blue;
        else winnerTeam = Team.None;

        yield return endGamePanel.ShowEndPanel(winnerTeam);
        yield return new WaitForSeconds(0.5f);
        yield return endGamePanel.HideEndPanel();

        ExitGames.Client.Photon.Hashtable gameStateProperty = new ExitGames.Client.Photon.Hashtable();
        gameStateProperty.Add(RoomProperty.GameState, State.Stop);
        PhotonNetwork.CurrentRoom.SetCustomProperties(gameStateProperty);
    }
}
