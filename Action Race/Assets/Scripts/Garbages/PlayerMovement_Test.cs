using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Test : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    Rigidbody2D rb;
    PhotonView pv;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector3 movementVelocity = new Vector3(x, y, 0) * 0.05f;
            transform.position += movementVelocity;
        }
    }
}
