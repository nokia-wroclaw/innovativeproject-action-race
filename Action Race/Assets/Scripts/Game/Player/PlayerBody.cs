using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public bool IsTouchingLadder { get; private set; }
    public bool GetNokiaShot;
    public float Timer;

    private void Update()
    {
        Timer += Time.deltaTime;
        ResetGetNokiaShot();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
            IsTouchingLadder = true;
        if (collision.tag == "ThrownNokia")
        {
            Timer = 0f;
            GetNokiaShot = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
            IsTouchingLadder = false;
    }

    void ResetGetNokiaShot()
    {
        if (Timer > 3f)
        {
            GetNokiaShot = false;
        }
    }
}
