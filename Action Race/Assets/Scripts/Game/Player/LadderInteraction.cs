using UnityEngine;
using Photon.Pun;

public class LadderInteraction : MonoBehaviour
{
    PlayerMovement pm;

    private void Awake()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Ladder")
        {
            pm.isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Ladder")
        {
            pm.isClimbing = false;
        }
    }
}
