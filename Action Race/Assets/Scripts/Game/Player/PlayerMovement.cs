using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 8f;

    [Header("References")]
    [SerializeField] GameObject playerCamera;
    [SerializeField] BoxCollider2D feet;
    [SerializeField] GameObject nickNameTag;

    Animator animator;
    PhotonView pv;
    Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();

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
        FlipSprite();
    }

    void Run()
    {
        float axis = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(axis * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > 0f;
        animator.SetBool("Run", playerHasHorizontalSpeed);
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

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > 0;
        if (playerHasHorizontalSpeed)
        {
            Vector3 flipScale = new Vector3(Mathf.Sign(rb.velocity.x), 1f, 1f);
            transform.localScale = flipScale;
            nickNameTag.transform.localScale = flipScale;
        }
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }
}
