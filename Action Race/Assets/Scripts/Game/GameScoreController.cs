using UnityEngine;
using Photon.Pun;

public class GameScoreController : MonoBehaviourPunCallbacks
{
    GameHUDPanel ghp;
    GameStateController gsc;

    void Awake()
    {
        ghp = FindObjectOfType<GameHUDPanel>();
        gsc = FindObjectOfType<GameStateController>();
    }

    void Start()
    {
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        UpdateBlueScore(customRoomProperties);
        UpdateRedScore(customRoomProperties);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        UpdateBlueScore(propertiesThatChanged);
        UpdateRedScore(propertiesThatChanged);
    }

    void UpdateBlueScore(ExitGames.Client.Photon.Hashtable properties)
    {
        object value;
        if (properties.TryGetValue(RoomProperty.BlueScore, out value))
        {
            int score = (int)value;
            ghp.UpdateScoreText(Team.Blue, score);

            if (score >= (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.ScoreLimit])
                StartCoroutine(gsc.EndGame());
        }
    }

    void UpdateRedScore(ExitGames.Client.Photon.Hashtable properties)
    {
        object value;
        if (properties.TryGetValue(RoomProperty.RedScore, out value))
        {
            int score = (int)value;
            ghp.UpdateScoreText(Team.Red, (int)score);

            if (score >= (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.ScoreLimit])
                StartCoroutine(gsc.EndGame());
        }
    }

    public void AddScore(Team team, int score)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        object value;

        switch (team)
        {
            case Team.Blue:
                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(RoomProperty.BlueScore, out value))
                    score += (int)value;

                hash.Add(RoomProperty.BlueScore, score);
                break;

            case Team.Red:
                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(RoomProperty.RedScore, out value))
                    score += (int)value;

                hash.Add(RoomProperty.RedScore, score);
                break;
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
