using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameLobby : MonoBehaviour
{
    [SerializeField] GameLobbyPanel glp;

    void Start()
    {
        int maxPlayersCount = PhotonNetwork.CurrentRoom.MaxPlayers;
        glp.UpdateMaxPlayersCountText(maxPlayersCount);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            glp.ToggleLobbyPanel();
        }
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
    }

    public void Test()
    {
        Player[] players = PhotonNetwork.PlayerList;
        foreach(Player p in players)
        {
            glp.AddPlayerToTeamPanel(Team.None, p.NickName);
        }
    }
}
