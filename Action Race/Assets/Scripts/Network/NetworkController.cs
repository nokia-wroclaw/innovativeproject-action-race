using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Button> networkButtons;

    string gameVersion = "2.4";

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ObjectExtension.DestroyAll();
    }

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
            Connect();

        if (!PhotonNetwork.IsConnectedAndReady)
            foreach (Button button in networkButtons)
                button.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        foreach (Button button in networkButtons)
            button.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        foreach (Button button in networkButtons)
            if(button)
                button.interactable = false;
    }

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }
}
