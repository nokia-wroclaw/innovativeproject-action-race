using UnityEngine;
using Photon.Pun;

public class Timer : MonoBehaviour
{
    ScoreBoard sb;

    int gameStatus;

    void Start()
    {
        sb = FindObjectOfType<ScoreBoard>();

        if(PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add(RoomProperty.StartTime, PhotonNetwork.Time);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
            Debug.Log((double)hash[RoomProperty.StartTime] + " " + PhotonNetwork.Time);
        }
        UpdateTime((double)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.GameTime]);
        gameStatus = 1;
    }

    void Update()
    {
        if (gameStatus == 1)
        {
            ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
            double time = PhotonNetwork.Time - (double)hash[RoomProperty.StartTime];
            time = (double)hash[RoomProperty.GameTime] - time;

            if(time > 0)
            {
                UpdateTime(time);
            }
            else
            {
                EndGame();
                Debug.Log("END");
            }
        }
    }

    void UpdateTime(double time)
    {
        Vector2Int gameTime = new Vector2Int((int)time / 60, (int)time % 60);
        sb.SetTime(gameTime);
    }

    void EndGame()
    {
        gameStatus = 2;

        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        int redScores = (int)hash[RoomProperty.RedScore];
        int blueScores = (int)hash[RoomProperty.BlueScore];

        Team winner;
        if (redScores > blueScores) winner = Team.Red;
        else if(blueScores > redScores) winner = Team.Blue;
        else winner = Team.None;


        foreach (PlayerTeam pt in FindObjectsOfType<PlayerTeam>())
        {
            PhotonView pv = pt.GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                Team team = pt.GetTeam();
                if (winner == team) sb.ShowWinPanel();
                else if (winner == Team.None) sb.ShowTiePanel();
                else sb.ShowLosePanel();
            }
        }


    }
}
