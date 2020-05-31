using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CountdownTimerLimitPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Dropdown countdownTimerLimitDropdown;
    [SerializeField] Text countdownTimerLimitText;

    List<int> countdownTimerLimits = new List<int>();

    public int Value
    {
        set 
        {
            int option = countdownTimerLimits.IndexOf(value);
            countdownTimerLimitDropdown.value = option;
            countdownTimerLimitText.text = countdownTimerLimitDropdown.options[option].text;
        }
    }

    public void ClearDropdown()
    {
        countdownTimerLimitDropdown.options.Clear();
    }

    public void AddDropdownValue(int minutes)
    {
        countdownTimerLimitDropdown.options.Add(new Dropdown.OptionData { text = minutes + " min"});
        countdownTimerLimits.Add(minutes);
    }

    public int GetCountdownTimerLimit(int option)
    {
        return countdownTimerLimits[option];
    }
}
