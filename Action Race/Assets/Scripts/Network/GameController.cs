using UnityEngine;
using Photon.Pun;

public class GameController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }

    void CreatePlayer()
    {
        Debug.Log("Create player");
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}
