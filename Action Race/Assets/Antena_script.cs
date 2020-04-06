using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antena_script : MonoBehaviour
{
    public int team;
    public Teams_script script;

    // Start is called before the first frame update
    void Start()
    {
        script = FindObjectOfType<Teams_script>();
        team = 999;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTeam(int team)
    {
        if(this.team != team)
        {
            this.team = team;

            SpriteRenderer sprite = GetComponent<SpriteRenderer>(); //temporary
            if (team == 0)
                sprite.color = new Color(1, 0, 0, 1);
            if (team == 1)
                sprite.color = new Color(0, 0, 1, 1);

            script.UpdatePoints();
        }
    }
}
