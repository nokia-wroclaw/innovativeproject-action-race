using UnityEngine;
using UnityEngine.UI;

public class GameHUDPanel : MonoBehaviour
{
    [SerializeField] Text redScoreText;
    [SerializeField] Text blueScoreText;
    [SerializeField] Text timeText;

    public void UpdateScoreText(Team team, int score)
    {
        switch(team)
        {
            case Team.Blue:
                blueScoreText.text = score.ToString();
                break;

            case Team.Red:
                redScoreText.text = score.ToString();
                break;
        }
    }

    public void UpdateTimeText(Vector2Int time)
    {
        string sTime = time.x.ToString() + " : ";
        if (time.y < 10) sTime +=  "0";
        sTime += time.y.ToString();

        timeText.text = sTime;
    }
}
