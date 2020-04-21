using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GamePlayManager : MonoBehaviourPunCallbacks
{
    ScoreBoard scoreBoard;

    void Start()
    {
        PhotonNetwork.FetchServerTimestamp();
        scoreBoard = FindObjectOfType<ScoreBoard>();

        UpdateTime((float)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.GameTime]);
    }

    void Update()
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        float time = Time.time - (float)hash[RoomProperty.StartTime];
        time = (float)hash[RoomProperty.GameTime] - time;
        UpdateTime(time);
    }

    public void SetScore(Team team, int score)
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        switch (team)
        {
            case Team.Blue:
                hash[RoomProperty.BlueScore] = (int)hash[RoomProperty.BlueScore] + score;
                break;

            case Team.Red:
                hash[RoomProperty.RedScore] = (int)hash[RoomProperty.RedScore] + score;
                break;

            default:
                return;
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void UpdateTime(double timeInSeconds)
    {
        Vector2Int gameTime = new Vector2Int((int)timeInSeconds / 60, (int)timeInSeconds % 60);
        scoreBoard.SetTime(gameTime);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        //object value;
        //if (propertiesThatChanged.TryGetValue(RoomProperty.RedScore, out value))
        //{
            
        //}
        //if (propertiesThatChanged.TryGetValue(RoomProperty.BlueScore, out value))
        //{
            
        //}

        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        scoreBoard.SetScore((int)hash[RoomProperty.RedScore], (int)hash[RoomProperty.BlueScore]);   
    }
}
