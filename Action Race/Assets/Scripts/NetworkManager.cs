using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI playerNickName;

    [Header("Join to the game")]
    [SerializeField] Button playButton;
    [SerializeField] int gameSceneIndex;
    [SerializeField] GameObject connectonProgress;
    [SerializeField] TextMeshProUGUI connectionProgressTMP;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void SetPlayerNickName()
    {
        Debug.Log("Set nickname to " + playerNickName.text);
        PhotonNetwork.LocalPlayer.NickName = playerNickName.text;
    }

    public void Play()
    {
        Debug.Log("Searching the match...");
        connectonProgress.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public void Cancel()
    {
        Debug.Log("Left the room");
        connectonProgress.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Create a room");
        int randomRoomNum = Random.Range(0, 1000);
        RoomOptions roomOpt = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)3 };
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
        connectionProgressTMP.text = "Joining to the room...";
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
