using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class QuickPlayController : MonoBehaviourPunCallbacks
{
    [SerializeField] ConnectionStatusController connectionStatusController;

    public void QuickPlay()
    {
        Debug.Log("QuickPlay");
        PhotonNetwork.JoinRandomRoom();

        connectionStatusController.ShowConnectionStatus();
        connectionStatusController.SetJoinRandomMessage();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room to join, create your own");

        connectionStatusController.SetJoinRandomFailedMessage();
        StartCoroutine(connectionStatusController.HideConnectionStatus());
    }
}
