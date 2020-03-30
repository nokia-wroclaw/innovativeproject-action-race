using Photon.Pun;
using UnityEngine;

public class PlayerInteraction : MonoBehaviourPun
{

    bool isInteract = false;
    bool isAntennaTriggered = false;

    private AntennaController antennaController;

    private void Awake()
    {
        
    }

    void Update()
    {
        antennaController = GameObject.FindObjectOfType<AntennaController>();
        //Debug.Log(antennaController.name);

        if (Input.GetButtonDown("Interact"))
        {
            isInteract = true;
            antennaController.animator.SetBool("isInteract", isInteract);
        }

        if (Input.GetButtonUp("Interact"))
        {
            isInteract = false;
            antennaController.animator.SetBool("isInteract", isInteract);
        }
        antennaController.animator.SetBool("isAntennaTriggered", isAntennaTriggered);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Antenna"))
        {
            isAntennaTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Antenna"))
        {
            isAntennaTriggered = false;
        }
    }

}
