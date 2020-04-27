using UnityEngine;
using Photon.Pun;

public class GameScore : MonoBehaviourPunCallbacks
{
    GameHUDPanel gh;

    void Start()
    {
        gh = FindObjectOfType<GameHUDPanel>();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;

        if (propertiesThatChanged.TryGetValue(RoomProperty.BlueScore, out value))
            gh.UpdateScoreText(Team.Blue, (int)value);

        if (propertiesThatChanged.TryGetValue(RoomProperty.RedScore, out value))
            gh.UpdateScoreText(Team.Red, (int)value);
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
}
