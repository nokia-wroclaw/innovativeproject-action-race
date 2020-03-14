using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class QuickPlayController : MonoBehaviourPunCallbacks
{
    public void QuickPlay()
    {
        Debug.Log("QuickPlay");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room to join");

        //FOR TESTS
        GetComponent<GameCreatorController>().CreateGame();
        //
    }
}
