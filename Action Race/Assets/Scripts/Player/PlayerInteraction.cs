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

        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteract = true;
            antennaController.animator.SetBool("isInteract", isInteract);
        }

        if (Input.GetKeyUp(KeyCode.E))
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
<<<<<<< Updated upstream
            isAntennaTriggered = true;
=======
            Antena_script antena = collision.GetComponent<Antena_script>();
            antena.UpdateTeam(GetComponent<PlayerTeam_script>().team);
            //Debug.Log(collision.name);
            //collision.gameObject.SetActive(false);
>>>>>>> Stashed changes
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
