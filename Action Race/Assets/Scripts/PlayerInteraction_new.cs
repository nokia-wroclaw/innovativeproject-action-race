using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction_new : MonoBehaviour
{
    Animator animator;
    PlayerMovement pm;
    AntennaController antenna;
    public bool interact = false;
    public bool isAntenaNear = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            interact = true;
        }
        else
        {   
            interact = false;
            pm.enabled = true;
        }

        if (isAntenaNear && interact)
        {
            if (antenna.whichTeam != GetComponent<PlayerTeam_script>().team)
            {
                pm.enabled = false;
                pm.StopRunning();
                animator.SetTrigger("Interact");

                antenna.UpdateTeam(GetComponent<PlayerTeam_script>().team);
                antenna.startTimer = true;
            }
        }
        else if (isAntenaNear && !interact)
        {
            pm.enabled = true;
            //animator.SetTrigger("Interact");
            if(!antenna.programed)
            {
                antenna.UpdateTeam(999);
            }
            antenna.startTimer = false;

        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Antenna"))
        {
            isAntenaNear = true;
            antenna = col.GetComponent<AntennaController>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        isAntenaNear = false;
        antenna = null;
    }
}
