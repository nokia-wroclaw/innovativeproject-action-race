using UnityEngine;
using Photon.Pun;

public class GameController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
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
        Vector3 position1 = new Vector3(1, 1, 0);
        PhotonNetwork.Instantiate("BasicAntenna", position1, Quaternion.identity);
    }
}
