using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Button> networkButtons;   

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        if(!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();

        if(!PhotonNetwork.IsConnectedAndReady)
            foreach (Button button in networkButtons)
                button.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        foreach(Button button in networkButtons)
            button.interactable = true;

        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
        foreach (Button button in networkButtons)
            if(button)
                button.interactable = false;
    }
}
