using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Text redScoreText;
    [SerializeField] Text blueScoreText;
    [SerializeField] Text timeText;

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject tiePanel;
    [SerializeField] GameObject losePanel;

    [SerializeField] Text nickNameText;

    public void SetRedScore(int redScore)
    {
        redScoreText.text = redScore.ToString();
    }

    public void SetBlueScore(int blueScore)
    {
        blueScoreText.text = blueScore.ToString();
    }

    public void SetTime(Vector2Int time)
    {
        if(time.y < 10) timeText.text = time.x + " : 0" + time.y;
        else timeText.text = time.x + " : " + time.y;
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void ShowTiePanel()
    {
        tiePanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }

    public void SetNickName(string nickName)
    {
        nickNameText.text = nickName;
    }
}
