using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    public bool OnTheGround { get; private set; }

    public bool IsTouchingLadder { get; private set; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
            OnTheGround = true;
        else if (collision.tag == "Ladder")
            IsTouchingLadder = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
            OnTheGround = false;
        else if (collision.tag == "Ladder")
            IsTouchingLadder = false;
    }
}
