using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaController : MonoBehaviour
{
    public Animator animator;

    public float timer = 3.5f;
    public bool startTimer = false;
    public int whichTeam = 999;
    bool isAntennaTriggered = false;
    public Teams_script script;
    public bool programed = false;


    void Start()
    {
        script = FindObjectOfType<Teams_script>();
        script.AddAntena(this);
        animator.SetInteger("whichTeam", whichTeam);
    }

    public void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            animator.SetTrigger("Program");
            if (whichTeam == 0)
            {
                animator.Play("Red_anim");
            }
            else if (whichTeam == 1)
            {
                animator.Play("Blue_anim");
            }

        }
        else if (!startTimer)
        {
            timer = 3.5f;
            animator.ResetTrigger("Program");
            animator.Play("Idle");
        }
            
        if(timer <= 0)
        {
            if (whichTeam == 0)
            {
                animator.Play("Red_done");
            }
            else if (whichTeam == 1)
            {
                animator.Play("Blue_done");
            }
            script.UpdatePoints();
            animator.ResetTrigger("Program");
            programed = true;
            startTimer = false;
            timer = 3.5f;
        }
    }

    public void UpdateTeam(int team)
    {
        if (this.whichTeam != team)
        {
            this.whichTeam = team;
        }
    }

}
