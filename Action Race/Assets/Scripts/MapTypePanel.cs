using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapTypePanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Dropdown mapTypeDropdown;
    [SerializeField] Text mapTypeText;

    List<string> mapTypes = new List<string>();

    public string Value
    {
        set
        {
            int option = mapTypes.IndexOf(value);
            mapTypeDropdown.value = option;
            mapTypeText.text = mapTypeDropdown.options[option].text;
        }
    }

    public void ClearDropdown()
    {
        mapTypeDropdown.options.Clear();
    }

    public void AddDropdownValue(string map)
    {
        mapTypeDropdown.options.Add(new Dropdown.OptionData { text = map });
        mapTypes.Add(map);
    }

    public string GetMapType(int option)
    {
        return mapTypes[option];
    }

    public void ConfigureAccess(bool isMasterClient, State state = State.Stop)
    {
        if (isMasterClient && state == State.Stop)
        {
            mapTypeDropdown.gameObject.SetActive(true);
            mapTypeText.gameObject.SetActive(false);
        }
        else
        {
            mapTypeDropdown.gameObject.SetActive(false);
            mapTypeText.gameObject.SetActive(true);
        }
    }
}
