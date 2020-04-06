using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams_script : MonoBehaviour
{
    //need script for antena

    public float Timer = 60;
    public List<PlayerTeam_script> players;
    public List<Antena_script> antenas;

    public int blueTeamScore;
    public int redTeamScore;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        //show timer on UI
        if(Timer <= 0)
        {
            //endgame
            Timer = 60;
        }
        //show points on UI
    }

    public void UpdatePoints()
    {
        int antLen = antenas.Count;

        for (int i = 0; i < antLen; i++)
        {
            if (antenas[i].team == 0)
                redTeamScore += 1;
            else if (antenas[i].team == 1)
                blueTeamScore += 1;
        }
    }

    public void addToArray(PlayerTeam_script s)
    {
        players.Add(s);
    }

    public int giveTeam(PlayerTeam_script p)
    {
        int index = players.IndexOf(p);

        if (index % 2 == 0)
            return 0;
        else if (index % 2 == 1)
            return 1;
        else
            return 0;
    }
}
