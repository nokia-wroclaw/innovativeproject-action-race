﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaController : MonoBehaviour
{
    public Animator animator;

    bool isInteract = false;
    bool isAntennaTriggered = false;
    int whichTeam = 2;
    //1 - red, 2 - blue

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // animator.SetBool("isInteract", isInteract);
       // animator.SetBool("isAntennaTriggered", isAntennaTriggered);
        animator.SetInteger("whichTeam", whichTeam);
    }
}