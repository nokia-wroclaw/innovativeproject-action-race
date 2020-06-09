using UnityEngine;
using Photon.Pun;

public class PlayerItems : MonoBehaviour
{
    bool hasNokia;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Nokia" && !hasNokia)
        {
            hasNokia = true;

        }
    }

    [PunRPC]
    void RaiseNokia()
    {
        //PhotonNetwork.Destroy(collision.gameObject);
    }
}
