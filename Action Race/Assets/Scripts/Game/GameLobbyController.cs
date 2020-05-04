using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameLobbyController : MonoBehaviourPunCallbacks
{
    public void ChangeTimeLimit(int time)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.TimeLimit, time);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void ChangeScoreLimit(int score)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.ScoreLimit, score);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    /*[SerializeField] GameLobbyPanel glp;

    void Start()
    {
        int maxPlayersCount = PhotonNetwork.CurrentRoom.MaxPlayers;
        glp.UpdateMaxPlayersCountText(maxPlayersCount);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            glp.Toggle();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        glp.AddPlayerToTeamPanel(Team.None, newPlayer.NickName);
    }   

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ChangeTimeLimit(int time)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.TimeLimit, (double)time);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void ChangeScoreLimit(int score)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.ScoreLimit, score);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }*/
}
