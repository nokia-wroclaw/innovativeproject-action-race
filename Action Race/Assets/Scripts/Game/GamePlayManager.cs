using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GamePlayManager : MonoBehaviourPunCallbacks
{
    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();

        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        scoreBoard.SetRedScore((int)hash[RoomProperty.RedScore]);
        scoreBoard.SetBlueScore((int)hash[RoomProperty.BlueScore]);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SetScore(Team team, int score)
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

            default:
                return;
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;
        if (propertiesThatChanged.TryGetValue(RoomProperty.RedScore, out value))
        {
            scoreBoard.SetRedScore((int)value);
        }
        if (propertiesThatChanged.TryGetValue(RoomProperty.BlueScore, out value))
        {
            scoreBoard.SetBlueScore((int)value);
        }
        Debug.Log(propertiesThatChanged.Count);
    }
}
