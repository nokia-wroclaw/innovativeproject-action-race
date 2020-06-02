using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text blueScoreText;
    [SerializeField] Text redScoreText;

    public int BlueScore
    {
        set
        {
            blueScoreText.text = value.ToString();
        }
    }

    public int RedScore
    {
        set
        {
            redScoreText.text = value.ToString();
        }
    }
}
