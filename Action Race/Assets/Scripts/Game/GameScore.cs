using UnityEngine;
using Photon.Pun;

public class GameScore : MonoBehaviourPunCallbacks
{
    GameHUDPanel ghp;
    GameState gs;

    void Start()
    {
        ghp = FindObjectOfType<GameHUDPanel>();
        gs = FindObjectOfType<GameState>();

        if (PhotonNetwork.IsMasterClient)
        {
            ResetScore();
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;

        if (propertiesThatChanged.TryGetValue(RoomProperty.BlueScore, out value))
        {
            int score = (int)value;
            ghp.UpdateScoreText(Team.Blue, score);

            if (score >= (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.ScoreLimit])
                StartCoroutine(gs.EndGame());
        }

        if (propertiesThatChanged.TryGetValue(RoomProperty.RedScore, out value))
        {
            int score = (int)value;
            ghp.UpdateScoreText(Team.Red, score);

            if (score >= (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.ScoreLimit])
                StartCoroutine(gs.EndGame());
        }

        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
        {
            State state = (State)value;
            switch (state)
            {
                case State.Play:
                    if (PhotonNetwork.IsMasterClient)
                        ResetScore();

                    break;
            }
        }
    }

    public void AddScore(Team team, int score)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        switch (team)
        {
            case Team.Blue:
                int blueScore = (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.BlueScore] + score;
                hash.Add(RoomProperty.BlueScore, blueScore);
                break;

            case Team.Red:
                int redScore = (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.RedScore] + score;
                hash.Add(RoomProperty.RedScore, redScore);
                break;
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    void ResetScore()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.BlueScore, 0);
        hash.Add(RoomProperty.RedScore, 0);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
