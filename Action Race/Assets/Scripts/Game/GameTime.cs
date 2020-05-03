using UnityEngine;
using Photon.Pun;
using System.Collections;

public class GameTime : MonoBehaviourPunCallbacks
{
    GameHUDPanel ghp;

    void Start()
    {
        ghp = FindObjectOfType<GameHUDPanel>();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;

        if (propertiesThatChanged.TryGetValue(RoomProperty.TimeLimit, out value))
            UpdateTime((int)value);

    }

    void UpdateTime(int time)
    {
        Vector2Int vTime = new Vector2Int((int)time / 60, (int)time % 60);
        ghp.UpdateTimeText(vTime);
    }

    /*GameHUDPanel ghp;
    GameState gs;

    bool countdown;

    void Start()
    {
        ghp = FindObjectOfType<GameHUDPanel>();
        gs = FindObjectOfType<GameState>();

        countdown = (State)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.GameState] == State.Play ? true : false;

        if (!PhotonNetwork.IsMasterClient) return;
        ResetTime();
    }

    void Update()
    {
        if (!countdown) return;

        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        double time = PhotonNetwork.Time - (double)hash[RoomProperty.StartTime];
        time = (double)hash[RoomProperty.TimeLimit] - time;

        if (time > 0)
        {
            UpdateTime(time);
        }
        else
        {
            StartCoroutine(EndGame());
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;

        if (propertiesThatChanged.TryGetValue(RoomProperty.StartTime, out value))
            UpdateTime((double)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.TimeLimit]);

        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
        {
            State state = (State)value;
            switch (state)
            {
                case State.Play:
                    if (PhotonNetwork.IsMasterClient)
                        ResetTime();

                    countdown = true;
                    break;

                case State.End:
                    countdown = false;
                    break;
            }
        }
    }

    void UpdateTime(double time)
    {
        Vector2Int vTime = new Vector2Int((int)time / 60, (int)time % 60);
        ghp.UpdateTimeText(vTime);
    }

    public void ResetTime()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.StartTime, PhotonNetwork.Time);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    IEnumerator EndGame()
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        int redScores = (int)hash[RoomProperty.RedScore];
        int blueScores = (int)hash[RoomProperty.BlueScore];

        Team winner;
        if (redScores > blueScores) winner = Team.Red;
        else if (blueScores > redScores) winner = Team.Blue;
        else winner = Team.None;

        foreach (PlayerTeam pt in FindObjectsOfType<PlayerTeam>())
        {
            PhotonView pv = pt.GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                Team team = pt.GetTeam();
                if (winner == team) ghp.ShowEndGamePanel(1);
                else if (winner == Team.None) ghp.ShowEndGamePanel(0);
                else ghp.ShowEndGamePanel(-1);
            }
        }

        yield return new WaitForSeconds(3f);

        ghp.HideEndGamePanel();
        gs.EndGame();
    }*/
}
