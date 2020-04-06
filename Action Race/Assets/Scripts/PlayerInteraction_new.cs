using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction_new : MonoBehaviour
{
    Animator animator;
    PlayerMovement pm;
    AntennaController antenna;
    bool interact = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        antenna = FindObjectOfType<AntennaController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interact = true;
            antenna.StartTime();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            interact = false;
            pm.enabled = true;
            antenna.StopTime();
            antenna.PlayAnimation();
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag("Antenna")) return;

        if (interact)
        {

            pm.enabled = false;
            pm.StopRunning();
            animator.SetTrigger("Interact");

            //Antena_script antena = col.GetComponent<Antena_script>();
            //antena.UpdateTeam(GetComponent<PlayerTeam_script>().team);

            AntennaController ac = col.GetComponent<AntennaController>();
            ac.ProgramAntenna();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        animator.ResetTrigger("Program");
        antenna.ResetIsAntennaTriggered();

    }
}
