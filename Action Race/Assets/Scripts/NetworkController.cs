using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] List<GameObject> networkButtons;   

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        foreach(GameObject go in networkButtons)
        {
            go.SetActive(true);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
        foreach (GameObject go in networkButtons)
        {
            go.SetActive(false);
        }
    }
}
