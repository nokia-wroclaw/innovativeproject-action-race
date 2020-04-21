﻿using UnityEngine;
using Photon.Pun;

public class GameController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();

        if (!PhotonNetwork.IsMasterClient) return;
        CreateAntennas();
    }

    void CreatePlayer()
    {
        Debug.Log("Create player");
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }

    void CreateAntennas()
    {
        Debug.Log("Create first antenna");
        Vector3 position1 = new Vector3(1, 1.45f, 0);
        PhotonNetwork.InstantiateSceneObject("BasicAntenna", position1, Quaternion.identity);
    }
}
