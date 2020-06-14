using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class PlayerThrow : MonoBehaviour
{
    PhotonView _photonView;

    bool raisedNokia;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!_photonView.IsMine)
            return;

        ThrowNokia();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Nokia" && !raisedNokia)
        {
            if (!_photonView.IsMine) return;

            Debug.Log("Podniosłeś Nokię");
            raisedNokia = true;

            _photonView.RPC("DestroyNokia", PhotonNetwork.MasterClient, collision.GetComponent<PhotonView>().ViewID);
        }

    }

    void ThrowNokia()
    {
        if (Input.GetButtonDown("Throw") && raisedNokia)
        {
            float dir = transform.localScale.x;
            Vector3 nokiaPosition = transform.position;
            nokiaPosition.x += dir * 0.7f; //zeby uniknac wlasnego collidera
            GameObject thrownNokia = PhotonNetwork.Instantiate("ThrownNokia", nokiaPosition, Quaternion.identity);
            thrownNokia.GetComponent<PhotonView>().RPC("Throw", RpcTarget.All, transform.localScale.x);
            raisedNokia = false;
        }
    }

    [PunRPC]
    public void DestroyNokia(int viewID)
    {
        PhotonNetwork.Destroy(PhotonNetwork.GetPhotonView(viewID));
    }
}
