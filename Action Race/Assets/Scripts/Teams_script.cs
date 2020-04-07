using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams_script : MonoBehaviour
{
    //need script for antena

    public List<PlayerTeam_script> players;
    public List<PlayerTeam_script> redTeam;
    public List<PlayerTeam_script> blueTeam;
    public List<AntennaController> antenas;
    public HUD_manager manager;

    public int blueTeamScore;
    public int redTeamScore;

    int redblue = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddAntena(AntennaController antena)
    {
        antenas.Add(antena);
    }

    public void UpdatePoints()
    {
        int antLen = antenas.Count;

        for (int i = 0; i < antLen; i++)
        {
            if (antenas[i].whichTeam == 0)
                redTeamScore += 1;
            else if (antenas[i].whichTeam == 1)
                blueTeamScore += 1;
        }

        manager.UpdatePoints(redTeamScore, blueTeamScore);
    }

    public void addToArray(PlayerTeam_script s)
    {
        players.Add(s);
        if (redblue == 0)
        {
            redblue = 1;
            redTeam.Add(s);
        }
        else if (redblue == 1)
        {
            redblue = 0;
            blueTeam.Add(s);
        }
        refreshColors();
    }

    public int giveTeam(PlayerTeam_script p)
    {
        /*
        int index = players.IndexOf(p);

        if (index % 2 == 0)
            return 0;
        else if (index % 2 == 1)
            return 1;
        else
            return 0;*/

        if(redTeam.Contains(p))
        {
            return 0;
        }
        else if (blueTeam.Contains(p))
        {
            return 1;
        }
        return 0;
    }

    void refreshColors()
    {
        int i;

        for (i = 0; i < redTeam.Count; i++)
        {
            redTeam[i].team = 0;
            redTeam[i].refreshColor();
        }

        for (i = 0; i < blueTeam.Count; i++)
        {
            blueTeam[i].team = 1;
            blueTeam[i].refreshColor();
        }
    }
}
