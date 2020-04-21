using UnityEngine;
using Photon.Pun;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    ScoreBoard scoreBoard;

    bool gameEnded;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();

        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.FetchServerTimestamp();
            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add(RoomProperty.StartTime, PhotonNetwork.Time);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }
        UpdateTime((double)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.GameTime]);
    }

    void Update()
    {
        if (!gameEnded)
        {
            ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
            double time = PhotonNetwork.Time - (double)hash[RoomProperty.StartTime];
            time = (double)hash[RoomProperty.GameTime] - time;

            if(time <= 0)
            {
                EndGame();
            }
            else
            {
                UpdateTime(time);
            }
        }
    }

    void UpdateTime(double time)
    {
        Vector2Int gameTime = new Vector2Int((int)time / 60, (int)time % 60);
        scoreBoard.SetTime(gameTime);
    }

    void EndGame()
    {
        gameEnded = true;

        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        int redScores = (int)hash[RoomProperty.RedScore];
        int blueScores = (int)hash[RoomProperty.BlueScore];

        Team winner;
        if (redScores > blueScores) winner = Team.Red;
        else if(blueScores > redScores) winner = Team.Blue;
        else winner = Team.None;

        foreach (PlayerTeam pt in FindObjectsOfType<PlayerTeam>())
        {
            Team team = pt.GetTeam();
            if (team == winner) winPanel.SetActive(true);
            else losePanel.SetActive(true);
        }
    }
}
