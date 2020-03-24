using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public bool isGrounded;

    float translation;
    public float facingdir;
    [SerializeField] GameObject mainCamera;

    private bool kick = false;

    Rigidbody2D rb;
    PhotonView pv;

    //animacja
    private Animator animator;
    //----------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();

        animator = GetComponent<Animator>();

        if(!pv.IsMine)
        {
            mainCamera.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && kick)
        {
            TemporaryEnemy_movement temp = col.gameObject.GetComponent<TemporaryEnemy_movement>();

            temp.GetKicked(facingdir);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
            isGrounded = true;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
            isGrounded = false;
    }

    void Update()
    {
        if (!pv.IsMine) return;

        if (Input.GetAxis("Horizontal") > 0)
            facingdir = 1;
        if (Input.GetAxis("Horizontal") < 0)
            facingdir = -1;

        // animacja biegania
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isRunning", true);
            animator.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning", true);
            animator.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
            animator.SetBool("isRunning", false);

        // animacja interakcji
        if(Input.GetKey(KeyCode.E))
            animator.SetBool("isInteracting", true);
         else
            animator.SetBool("isInteracting", false);

        // animacja skakania
        if(Input.GetKeyDown(KeyCode.Space))
            animator.SetBool("isJumping", true);
        else
            animator.SetBool("isJumping", false);


        translation = Input.GetAxis("Horizontal") * speed;

        translation *= Time.deltaTime;

        transform.Translate(translation, 0, 0);

        if (Input.GetKeyDown(KeyCode.K))
            kick = true;
        if (Input.GetKeyUp(KeyCode.K))
            kick = false;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce((new Vector3(0.0f, 2.0f, 0.0f)) * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    public void GetKicked(float dir)
    {
        rb.AddForce((new Vector3(dir * 2.0f, 2.0f, 0.0f)), ForceMode2D.Impulse);
    }
}
