using UnityEngine;
using UnityEngine.UI;

public class CountdownTimerPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text countdownTimerText;

    public double CountdownTimer
    {
        set
        {
            int minutes = (int)value / 60;
            int seconds = (int)value % 60;
            countdownTimerText.text = minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
        }
    }
}
