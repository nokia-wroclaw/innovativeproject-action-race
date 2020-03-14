using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections;

public class MatchMakingManager : MonoBehaviourPunCallbacks
{
    /*[Header("Room Properties")]
    [SerializeField] CreateGamePanel createGamePanel;
    

    [Header("Room Connection")]
    [SerializeField] int roomSceneIndex;

    [Header("Connection Status")]
    [SerializeField] GameObject connectionStatusPanel;
    [SerializeField] TextMeshProUGUI connectionStatusTMP;

    public void QuickPlay()
    {
        Debug.Log("Quick Play");
        PhotonNetwork.JoinRandomRoom();
    }

    public void CreateGame()
    {
        createGamePanel.SetActive(true);
    }

    public void CreateRoom()
    {
        string serverName = createGamePanel.GetServerName();
        int teamsNumber = createGamePanel.GetTeamSize();
        int teamSize = createGamePanel.GetTeamSize();

        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)(teamsNumber * teamSize) };
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add(RoomProperty.Password, "");
        roomOps.CustomRoomProperties.Add(RoomProperty.TeamsNumber, teamsNumber);
        roomOps.CustomRoomProperties.Add(RoomProperty.TeamSize, teamSize);
        roomOps.CustomRoomProperties.Add(RoomProperty.Mode, "");
        roomOps.CustomRoomProperties.Add(RoomProperty.Map, "");

        Debug.Log("CreateRoom");
        connectionStatusTMP.text = "Creating the room...";
        connectionStatusPanel.SetActive(true);
        PhotonNetwork.CreateRoom(serverName, roomOps);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        connectionStatusTMP.text = "Room created";
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        connectionStatusTMP.text = "Joining to the room...";
        PhotonNetwork.LoadLevel(roomSceneIndex);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
        connectionStatusTMP.text = "Joining to the room...";
    }*/
}
