using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Button playButton;
    [SerializeField] int gameSceneIndex;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void PlayGame()
    {
        Debug.Log("Searching the match...");
        PhotonNetwork.JoinRandomRoom();
    }

    void CreateRoom()
    {
        int randomRoomNum = Random.Range(0, 1000);
        RoomOptions roomOpt = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 0 };
        PhotonNetwork.CreateRoom("room" + randomRoomNum, roomOpt);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.AutomaticallySyncScene = true;

        playButton.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined");
        PhotonNetwork.LoadLevel(gameSceneIndex);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
}
