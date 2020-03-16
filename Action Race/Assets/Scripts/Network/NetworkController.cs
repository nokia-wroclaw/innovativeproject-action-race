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
        if(PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState == ExitGames.Client.Photon.PeerStateValue.Disconnected)
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
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
        foreach (Button button in networkButtons)
            button.interactable = false;
    }
}
