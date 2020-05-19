using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 8f;

    [Header("References")]
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject nickNameTag;

    Animator animator;
    PhotonView pv;
    Rigidbody2D rb;

    PlayerBody playerBody;
    PlayerFeet playerFeet;

    void Awake()
    {
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();

        playerBody = GetComponentInChildren<PlayerBody>();
        playerFeet = GetComponentInChildren<PlayerFeet>();
    }

    void Start()
    {
        if (pv.IsMine)
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Me";
        else
            playerCamera.SetActive(false);
    }

    void Update()
    {
        nickNameTag.transform.localScale = transform.localScale;

        if (!pv.IsMine) return;
        Run();
        Jump();
        Climb();
        FlipSprite();
    }

    void Run()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal") * runSpeed;
        Vector2 playerVelocity = new Vector2(horizontalSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(horizontalSpeed) > 0f;
        animator.SetBool("Run", playerHasHorizontalSpeed);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && playerFeet.OnTheGround)
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            rb.velocity = jumpVelocity;
            animator.SetTrigger("Jump");
        }
    }

    void Climb()
    {
        float verticalSpeed = Input.GetAxis("Vertical") * runSpeed;

        if (playerBody.IsTouchingLadder)
        {
            animator.SetBool("Ladder", true);
            if (Mathf.Abs(verticalSpeed) > 0)
                animator.speed = 1;
            else
                animator.speed = 0;
        }
        else
        {
            animator.SetBool("Ladder", false);
            animator.speed = 1;
        }

        if (playerFeet.IsTouchingLadder)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
        }
        else
            rb.gravityScale = 5;
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > 0;
        if (playerHasHorizontalSpeed)
        {
            Vector3 flipScale = new Vector3(Mathf.Sign(rb.velocity.x), 1f, 1f);
            transform.localScale = flipScale;
        }
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }
}