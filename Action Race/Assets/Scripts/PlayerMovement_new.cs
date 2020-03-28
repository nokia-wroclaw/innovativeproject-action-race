using UnityEngine;
using Photon.Pun;

public class PlayerMovement_new : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 8f;

    [Header("")]
    [SerializeField] GameObject playerCamera;
    [SerializeField] BoxCollider2D feet;

    PhotonView pv;
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (!pv.IsMine)
        {
            playerCamera.SetActive(false);
        }
    }

    void Update()
    {
        if (!pv.IsMine) return;

        Run();
        Jump();
        Interact();

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
        if (!feet.IsTouchingLayers()) return;

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            rb.velocity = jumpVelocity;
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("isInteracting", true);
        }
        else
        {
            animator.SetBool("isInteracting", false);
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
}
