using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaController : MonoBehaviour
{
    public Animator animator;

    float timer = 0.0f;
    bool startTimer = false;
    Antena_script asc;
    //not nessesary, will work with asc.team 0-red, 1-blue
    int whichTeam = 1;
    //1 - red, 2 - blue
    bool isAntennaTriggered = false;
    void Start()
    {
        asc = this.GetComponent<Antena_script>();
        animator.SetInteger("whichTeam", whichTeam);
        //whichTeam = asc.team;
    }

    public void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
        }
    }

    public void ProgramAntenna()
    {
        animator.SetTrigger("Program");
        isAntennaTriggered = true;
    }

    public void StartTime()
    {
        timer = 0.0f;
        startTimer = true;
    }

    public void StopTime()
    {
        startTimer = false;
    }

    public void PlayAnimation()
    {
        if (timer < 3.5f && isAntennaTriggered)
        {
            animator.Play("Idle");
            animator.ResetTrigger("Program");
        }
        else if (timer > 3.5f && isAntennaTriggered)
        {
            if (whichTeam == 1)
            {
                animator.Play("Red_done");
            }
            else if (whichTeam == 2)
            {
                animator.Play("Blue_done");
            }
            animator.ResetTrigger("Program");
        }
        Debug.Log(timer);
        timer = 0.0f;
    }

    public void ResetIsAntennaTriggered()
    {
        isAntennaTriggered = false;
    }

}
