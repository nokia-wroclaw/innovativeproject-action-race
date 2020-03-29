
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    bool isInteract = false;


    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            isInteract = true;
        }

        if (Input.GetButtonUp("Interact"))
        {
            isInteract = false;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "BasicAntenna" && isInteract)
        {
            Debug.Log(collision.name);
            collision.gameObject.SetActive(false);
        }
    }


}
