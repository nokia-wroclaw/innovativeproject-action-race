using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreLimitPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Dropdown scoreLimitDropdown;
    [SerializeField] Text scoreLimitText;

    List<int> scoreLimits = new List<int>();

    public int Value
    {
        set
        {
            int option = scoreLimits.IndexOf(value);
            scoreLimitDropdown.value = option;
            scoreLimitText.text = scoreLimitDropdown.options[option].text;
        }
    }

    public void ClearDropdown()
    {
        scoreLimitDropdown.options.Clear();
    }

    public void AddDropdownValue(int scores)
    {
        scoreLimitDropdown.options.Add(new Dropdown.OptionData { text = scores.ToString() });
        scoreLimits.Add(scores);
    }

    public int GetScoreLimit(int option)
    {
        return scoreLimits[option];
    }

    public void ConfigureAccess(bool isMasterClient, State state = State.Stop)
    {
        if (isMasterClient && state == State.Stop)
        {
            scoreLimitDropdown.gameObject.SetActive(true);
            scoreLimitText.gameObject.SetActive(false);
        }
        else
        {
            scoreLimitDropdown.gameObject.SetActive(false);
            scoreLimitText.gameObject.SetActive(true);
        }
    }
}
