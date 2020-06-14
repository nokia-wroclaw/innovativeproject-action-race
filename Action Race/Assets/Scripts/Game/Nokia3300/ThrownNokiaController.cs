using UnityEngine;
using Photon.Pun;

public class ThrownNokiaController : MonoBehaviour
{
    [SerializeField] float throwSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;

    PhotonView _photonView;
    Rigidbody2D _rigidbody;

    void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    [PunRPC]
    void Throw(float dir)
    {
        _rigidbody.velocity = new Vector3(dir * throwSpeed, 0, 0);
    }

    void Update()
    {
        transform.Rotate(Vector3.back, rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        Debug.Log("Wykryto kolizję");
        //_photonView.RPC("DestroyNokia", PhotonNetwork.MasterClient, _photonView.ViewID);

        if(_photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }
}
