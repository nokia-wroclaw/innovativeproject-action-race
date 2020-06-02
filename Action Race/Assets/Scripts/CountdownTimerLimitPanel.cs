using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CountdownTimerLimitPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Dropdown countdownTimerLimitDropdown;
    [SerializeField] Text countdownTimerLimitText;

    List<double> countdownTimerLimits = new List<double>();

    public double Value
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

    public double GetCountdownTimerLimit(int option)
    {
        return countdownTimerLimits[option];
    }

    public void ConfigureAccess(bool isMasterClient, State state = State.Stop)
    {
        if(isMasterClient && state == State.Stop)
        {
            countdownTimerLimitDropdown.gameObject.SetActive(true);
            countdownTimerLimitText.gameObject.SetActive(false);
        }
        else
        {
            countdownTimerLimitDropdown.gameObject.SetActive(false);
            countdownTimerLimitText.gameObject.SetActive(true);
        }
    }
}
