using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 8f;
    [SerializeField] float kickCooldown = 1f;

    [Header("References")]
    [SerializeField] GameObject playerCamera;
    [SerializeField] BoxCollider2D feet;
    [SerializeField] CircleCollider2D foot;

    PhotonView pv;
    Rigidbody2D rb;
    Animator animator;

    float kickDelay = 0f;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (pv.IsMine)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Me";
        }
        else
        {
            playerCamera.SetActive(false);
        }

    }

    void Update()
    {
        if (!pv.IsMine) return;

        Run();
        Jump();
        Kick();

        FlipSprite();
    }

    void Run()
    {
        float axis = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(axis * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > 0f;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && feet.IsTouchingLayers())
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            rb.velocity = jumpVelocity;
            animator.SetTrigger("Jump");
        }
    }

    void Kick()
    {
        if (kickDelay > 0f)
        {
            kickDelay -= Time.deltaTime;
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                kickDelay = kickCooldown;
                animator.SetTrigger("Kick");

                int layerMask = LayerMask.GetMask("Player");
                List<Collider2D> colliders = new List<Collider2D>();
                foot.OverlapCollider(new ContactFilter2D() { layerMask = layerMask }, colliders);
                foreach (Collider2D collider in colliders)
                {
                    PhotonView pvOther = collider.GetComponentInParent<PhotonView>();
                    if(pvOther) pv.RPC("TakeKick", RpcTarget.All, pvOther.ViewID);
                    Debug.Log("kick");
                }
            }
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > 0;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1f, 1f);
        }
    }

    [PunRPC]
    void TakeKick(int viewId)
    {
        PhotonNetwork.GetPhotonView(viewId).GetComponent<Rigidbody2D>().velocity += new Vector2(0, 30);
    }
}
