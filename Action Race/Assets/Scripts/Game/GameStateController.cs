﻿using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class GameStateController : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform[] antennasWaypoints;
    [SerializeField] Transform[] laddersWaypoints;
    [SerializeField] Transform[] blueTeamSpawns;
    [SerializeField] Transform[] redTeamSpawns;
    [SerializeField] GameObject viewCamera;

    GameLobbyPanel glp;
    GameHUDPanel ghp;

    void Start()
    {
        glp = FindObjectOfType<GameLobbyPanel>();
        ghp = FindObjectOfType<GameHUDPanel>();

        Application.runInBackground = true;

        Synchronize();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
        {
            State state = (State)value;
            switch (state)
            {
                case State.NotStarted:
                    StopGame();
                    break;

                case State.Play:
                    SetGame();
                    break;
            }
        }
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    void Synchronize()
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        object value;

        if (hash.TryGetValue(RoomProperty.GameState, out value))
        {
            switch ((State)value)
            {
                case State.NotStarted:
                    StopGame();
                    break;

                case State.Play:
                    SetGame();
                    break;
            }
        }
        else
            StopGame();
    }

    void StopGame()
    {
        viewCamera.SetActive(true);

        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.DestroyAll();

        glp.SetActive(true);
    }

    void SetGame()
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        object value;

        if (hash.TryGetValue(PlayerProperty.Team, out value))
        {
            int spawnerId;
            switch ((Team)value)
            {
                case Team.Blue:
                    spawnerId = Random.Range(0, blueTeamSpawns.Length);
                    PhotonNetwork.Instantiate("Player", blueTeamSpawns[spawnerId].position, Quaternion.identity);
                    viewCamera.SetActive(false);
                    glp.SetActive(false);
                    break;

                case Team.Red:
                    spawnerId = Random.Range(0, redTeamSpawns.Length);
                    PhotonNetwork.Instantiate("Player", redTeamSpawns[spawnerId].position, Quaternion.identity);
                    viewCamera.SetActive(false);
                    glp.SetActive(false);
                    break;
            }
        }

        foreach (Transform waypoint in antennasWaypoints)
        {
            PhotonNetwork.InstantiateSceneObject("BasicAntenna", waypoint.position, Quaternion.identity);
        }

        foreach (Transform waypoint in laddersWaypoints)
        {
            PhotonNetwork.InstantiateSceneObject("Ladder", waypoint.position, Quaternion.identity);
        }
    }

    public IEnumerator EndGame()
    {
        ExitGames.Client.Photon.Hashtable playerHash = PhotonNetwork.LocalPlayer.CustomProperties;
        object value1;
        Team team;

        if (playerHash.TryGetValue(PlayerProperty.Team, out value1))
            team = (Team)value1;
        else
            team = Team.None;

        if (team == Team.None) yield break;


        ExitGames.Client.Photon.Hashtable roomHash = PhotonNetwork.CurrentRoom.CustomProperties;
        object value;
        int redScores, blueScores;

        if (roomHash.TryGetValue(RoomProperty.RedScore, out value))
            redScores = (int)value;
        else
            redScores = 0;

        if (roomHash.TryGetValue(RoomProperty.BlueScore, out value))
            blueScores = (int)value;
        else
            blueScores = 0;

        Team winner;
        if (redScores > blueScores) winner = Team.Red;
        else if (blueScores > redScores) winner = Team.Blue;
        else winner = Team.None;

        if (winner == team) ghp.ShowEndGamePanel(1);
        else if (winner == Team.None) ghp.ShowEndGamePanel(0); 
        else ghp.ShowEndGamePanel(-1);

        yield return new WaitForSeconds(3f);

        ghp.HideEndGamePanel();

        if(PhotonNetwork.IsMasterClient)
            glp.ChangeGameState(0);
    }
}
