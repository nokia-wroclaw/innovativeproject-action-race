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

    bool isRunning;
    bool isJumping;
    bool isClimbing, isStayingOnLadder;

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
        SettingsController settingsController = FindObjectOfType<SettingsController>();
        if (settingsController)
        {
            audioSource.mute = settingsController.Mute;
            audioSource.volume = settingsController.Volume;
        }

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
        Climb();
        Jump();
        FlipSprite();
        Animate();
        FreezeMovement();
    }

    void Run()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal");
        bool playerHasHorizontalSpeed = Mathf.Abs(horizontalSpeed) > 0f;
        if (playerHasHorizontalSpeed)
        {
            Vector2 playerVelocity = new Vector2(horizontalSpeed * runSpeed, rigidBody.velocity.y);
            rigidBody.velocity = playerVelocity;
        }
        else if (playerFeet.OnTheGround || playerFeet.IsTouchingLadder)
        {
            Vector2 playerVelocity = new Vector2(0f, rigidBody.velocity.y);
            rigidBody.velocity = playerVelocity;
        }

        isRunning = playerHasHorizontalSpeed;
    }

    void Climb()
    {
        float verticalSpeed = Input.GetAxis("Vertical");
        bool playerHasVerticalSpeed = Mathf.Abs(verticalSpeed) > 0f;

        isClimbing = playerBody.IsTouchingLadder && playerHasVerticalSpeed;
        isStayingOnLadder = playerBody.IsTouchingLadder && !playerHasVerticalSpeed;

        bool isClimbingMovement = playerFeet.IsTouchingLadder && playerHasVerticalSpeed;
        if (isClimbingMovement || isStayingOnLadder)
        {
            rigidBody.gravityScale = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, verticalSpeed * runSpeed);
        }
        else
            rigidBody.gravityScale = 5;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isClimbing && !isStayingOnLadder && playerFeet.OnTheGround)
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            rigidBody.velocity = jumpVelocity;
            isJumping = true;
            audioSource.PlayOneShot(jumpSound);
        }
        else
            isJumping = false;
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

    void Animate()
    {
        animator.SetBool("Run", isRunning);

        animator.SetBool("Climb", isClimbing);
        animator.SetBool("Stay On Ladder", isStayingOnLadder);

        if (isJumping)
            animator.SetTrigger("Jump");
    }

    public void StopMovement()
    {
        rigidBody.velocity = Vector2.zero;
    }

    public void FreezeMovement()
    {
        if (playerBody.GetNokiaShot)
        {
            StopMovement();
            //animator.Play("Idle", 0);
        }
    }
}