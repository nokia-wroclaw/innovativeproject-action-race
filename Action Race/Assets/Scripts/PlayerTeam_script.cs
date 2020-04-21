using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam_script : MonoBehaviour
{
    public int team;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        Teams_script t = Teams_script.FindObjectOfType<Teams_script>();
        sprite = GetComponent<SpriteRenderer>();
        //t.addToArray(this);
        //team = t.giveTeam(this);


        //refreshColor();
    }

    public void refreshColor()
    {
        if (team == 0)
            sprite.color = new Color(1, 0, 0, 1);
        if (team == 1)
            sprite.color = new Color(0, 0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
