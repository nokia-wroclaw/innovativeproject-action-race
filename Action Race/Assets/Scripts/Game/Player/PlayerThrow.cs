using UnityEngine;
using Photon.Pun;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] float throwSpeed = 0.04f;

    bool raisedNokia;
    GameObject thrownNokia;
    Vector2 initialPosition;

    PhotonView pv;
    PlayerMovement pm;
    Rigidbody2D rb;

    void Start()
    {
        raisedNokia = false;
        pv = GetComponent<PhotonView>();
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!pv.IsMine)
            return;

        ThrowNokia();
        UpdatePositionValue();
        UpdateNokiasPosition();
        CheckCollisions();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Nokia" && !raisedNokia)
        {
            Debug.Log("Podniosłeś Nokię");
            //collision.gameObject.SetActive(false);
            PhotonNetwork.Destroy(collision.gameObject);

            raisedNokia = true;
        }

    }

    void ThrowNokia()
    {
        if (Input.GetButtonDown("Throw") && raisedNokia)
        {
            initialPosition = pm.transform.position;
            initialPosition.x += 0.7f; //zeby uniknac wlasnego collidera
            thrownNokia = PhotonNetwork.Instantiate("ThrownNokia", initialPosition, Quaternion.identity);
            raisedNokia = false;

            rb = thrownNokia.GetComponent<Rigidbody2D>();
        }
    }

    void UpdatePositionValue()
    {
        if (thrownNokia)
        {
            initialPosition.x += throwSpeed;
        }
    }

    void UpdateNokiasPosition()
    {
        if (thrownNokia)
        {
            rb.MovePosition(initialPosition);

        }
    }

    void CheckCollisions()
    {
        if (thrownNokia)
        {
            ThrownNokiaController tnc = thrownNokia.GetComponent<ThrownNokiaController>();

            if (tnc.collided)
            {
                //thrownNokia.SetActive(false);
                PhotonNetwork.Destroy(thrownNokia);
                thrownNokia = null;
            }
        }

    }
}
