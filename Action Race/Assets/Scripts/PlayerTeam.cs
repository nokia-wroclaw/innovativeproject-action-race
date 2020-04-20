using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    SpriteRenderer sr;

    Team team = Team.Red;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        RefreshColor();
    }

    public void SetTeam(Team team)
    {
        this.team = team;
        RefreshColor();
    }

    public Team GetTeam()
    {
        return team;
    }

    public void RefreshColor()
    {
        if (team == Team.Red)
            sr.color = new Color(1, 0, 0, 1);
        if (team == Team.Blue)
            sr.color = new Color(0, 0, 1, 1);
    }
}
