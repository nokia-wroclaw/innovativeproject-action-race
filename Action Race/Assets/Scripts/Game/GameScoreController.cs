using UnityEngine;
using Photon.Pun;

public class GameScoreController : MonoBehaviourPunCallbacks
{
    GameHUDPanel ghp;
    GameStateController gsc;

    void Start()
    {
        ghp = FindObjectOfType<GameHUDPanel>();
        gsc = FindObjectOfType<GameStateController>();

        Synchronize();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;

        if (propertiesThatChanged.TryGetValue(RoomProperty.BlueScore, out value))
        {
            int score = (int)value;
            ghp.UpdateScoreText(Team.Blue, score);

            if (score >= (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.ScoreLimit])
                StartCoroutine(gsc.EndGame());
        }

        if (propertiesThatChanged.TryGetValue(RoomProperty.RedScore, out value))
        {
            int score = (int)value;
            ghp.UpdateScoreText(Team.Red, score);

            if (score >= (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.ScoreLimit])
                StartCoroutine(gsc.EndGame());
        }

        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
        {
            ResetScore();
        }
    }

    void Synchronize()
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        object value;

        if (hash.TryGetValue(RoomProperty.BlueScore, out value))
            ghp.UpdateScoreText(Team.Blue, (int)value);
        else
            ghp.UpdateScoreText(Team.Blue, 0);

        if (hash.TryGetValue(RoomProperty.RedScore, out value))
            ghp.UpdateScoreText(Team.Red, (int)value);
        else
            ghp.UpdateScoreText(Team.Red, 0);
    }

    public void AddScore(Team team, int score)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        object value;

        switch (team)
        {
            case Team.Blue:
                if (hash.TryGetValue(RoomProperty.BlueScore, out value))
                    score += (int)value;

                hash.Add(RoomProperty.BlueScore, score);
                break;

            case Team.Red:
                if (hash.TryGetValue(RoomProperty.RedScore, out value))
                    score += (int)value;

                hash.Add(RoomProperty.RedScore, score);
                break;
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    void ResetScore()
    {
        if (PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.BlueScore, 0);
        hash.Add(RoomProperty.RedScore, 0);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
