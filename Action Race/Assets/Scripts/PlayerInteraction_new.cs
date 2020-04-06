using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction_new : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag("Antenna")) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Interact");

            Antena_script antena = col.GetComponent<Antena_script>();
            antena.UpdateTeam(GetComponent<PlayerTeam_script>().team);

            AntennaController ac = col.GetComponent<AntennaController>();
            ac.ProgramAntenna();
        }
    }
}
