using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;

    public void SetScore(int redScore, int blueScore)
    {
        scoreText.text = redScore + " : " + blueScore;
    }

    public void SetTime(Vector2Int time)
    {
        timeText.text = time.x + " : " + time.y;
    }
}
