using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaController : MonoBehaviour
{
    public Animator animator;

    bool isInteract = false;
    bool isAntennaTriggered = false;
    int whichTeam = 2;
    //1 - red, 2 - blue


    public void ProgramAntenna()
    {
        animator.SetTrigger("Program");
    }
}
