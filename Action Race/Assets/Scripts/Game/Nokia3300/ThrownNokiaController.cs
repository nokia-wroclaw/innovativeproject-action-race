using UnityEngine;
using Photon.Pun;

public class ThrownNokiaController : MonoBehaviour
{
    [SerializeField] float throwSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;

    PhotonView _photonView;
    Rigidbody2D _rigidbody;

    float _dir;
    bool isRotating;

    void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isRotating)
            transform.Rotate(Vector3.back, rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_photonView.IsMine)
        {
            if(collision.tag == "Player")
            {
                Debug.Log("Player hit");
                PhotonView playerPhotonView = collision.GetComponentInParent<PhotonView>();
                playerPhotonView.RPC("Freeze", collision.GetComponentInParent<PhotonView>().Owner);
                playerPhotonView.RPC("GetKick", RpcTarget.AllViaServer, _dir);
            }

            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void Throw(float dir)
    {
        _dir = dir;
        _rigidbody.velocity = new Vector3(dir * throwSpeed, 0, 0);
        isRotating = true;
    }
}
