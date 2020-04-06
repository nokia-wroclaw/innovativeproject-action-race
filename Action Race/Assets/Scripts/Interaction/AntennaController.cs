using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaController : MonoBehaviour
{
    public Animator animator;

    bool isInteract = false;
    bool isAntennaTriggered = false;
    Antena_script asc;
    //not nessesary, will work with asc.team 0-red, 1-blue
    int whichTeam = 2;
    //1 - red, 2 - blue
    void Start()
    {
        asc = this.GetComponent<Antena_script>();
    }

    public void ProgramAntenna()
    {
        animator.SetTrigger("Program");
    }
}
