using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 8f;
    [SerializeField] AudioClip jumpSound;

    [Header("References")]
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject nickNameTag;

    Animator animator;
    AudioSource audioSource;
    PhotonView photonView;
    Rigidbody2D rigidBody;

    PlayerBody playerBody;
    PlayerFeet playerFeet;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>();
        rigidBody = GetComponent<Rigidbody2D>();

        playerBody = GetComponentInChildren<PlayerBody>();
        playerFeet = GetComponentInChildren<PlayerFeet>();
    }

    void Start()
    {
        if (photonView.IsMine)
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Me";
        else
            playerCamera.SetActive(false);
    }

    void Update()
    {
        nickNameTag.transform.localScale = transform.localScale;

        if (!photonView.IsMine) return;
        Run();
        Jump();
        Climb();
        FlipSprite();
    }

    void Run()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal") * runSpeed;
        Vector2 playerVelocity = new Vector2(horizontalSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(horizontalSpeed) > 0f;
        animator.SetBool("Run", playerHasHorizontalSpeed);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && playerFeet.OnTheGround)
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            rigidBody.velocity = jumpVelocity;
            animator.SetTrigger("Jump");
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void Climb()
    {
        float verticalSpeed = Input.GetAxis("Vertical") * runSpeed;

        if (playerBody.IsTouchingLadder)
        {
            animator.SetBool("Climb", true);
            if (Mathf.Abs(verticalSpeed) > Mathf.Epsilon)
                animator.SetBool("Stay On Ladder", false);
            else
                animator.SetBool("Stay On Ladder", true);
        }
        else
        {
            animator.SetBool("Climb", false);
            animator.SetBool("Stay On Ladder", false);
        }

        if (playerFeet.IsTouchingLadder)
        {
            rigidBody.gravityScale = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, verticalSpeed);
        }
        else
            rigidBody.gravityScale = 5;
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > 0;
        if (playerHasHorizontalSpeed)
        {
            Vector3 flipScale = new Vector3(Mathf.Sign(rigidBody.velocity.x), 1f, 1f);
            transform.localScale = flipScale;
        }
    }

    public void StopMovement()
    {
        rigidBody.velocity = Vector2.zero;
    }
}