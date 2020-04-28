using UnityEngine;
using Photon.Pun;

public class GameScore : MonoBehaviourPunCallbacks
{
    GameHUDPanel ghp;

    void Start()
    {
        ghp = FindObjectOfType<GameHUDPanel>();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;

        if (propertiesThatChanged.TryGetValue(RoomProperty.BlueScore, out value))
            ghp.UpdateScoreText(Team.Blue, (int)value);

        if (propertiesThatChanged.TryGetValue(RoomProperty.RedScore, out value))
            ghp.UpdateScoreText(Team.Red, (int)value);
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

    public void ResetScore()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.BlueScore, 0);
        hash.Add(RoomProperty.RedScore, 0);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
