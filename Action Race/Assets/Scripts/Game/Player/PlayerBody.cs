using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public bool IsTouchingLadder { get; private set; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
            IsTouchingLadder = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
            IsTouchingLadder = false;
    }
}
