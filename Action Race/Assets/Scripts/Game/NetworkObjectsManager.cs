using UnityEngine;
using Photon.Pun;

public class NetworkObjectsManager : MonoBehaviour
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
        PhotonNetwork.Instantiate("Player", Vector3.up * -4f, Quaternion.identity);
    }

    void CreateAntennas()
    {
        Debug.Log("Create antenna");
        Vector3 position = new Vector3(-2.5f, -3.6f, 0);
        PhotonNetwork.InstantiateSceneObject("BasicAntenna", position, Quaternion.identity);

        Vector3 position1 = new Vector3(2.5f, -3.6f, 0);
        PhotonNetwork.InstantiateSceneObject("BasicAntenna", position1, Quaternion.identity);
    }
}
