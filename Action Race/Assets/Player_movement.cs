using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public bool isGrounded;
    Rigidbody2D rb;

    float translation;
    public float facingdir;

    private bool kick = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0)
            facingdir = 1;
        if (Input.GetAxis("Horizontal") < 0)
            facingdir = -1;

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
