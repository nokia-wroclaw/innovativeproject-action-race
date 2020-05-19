using UnityEngine;
using UnityEngine.UI;

public class RoomTemplate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image passwordImage;
    [SerializeField] Text roomNameText;
    [SerializeField] Text ownerText;
    [SerializeField] Text mapText;
    [SerializeField] Text playersText;
    [SerializeField] Text maxPlayersText;

    string _password;

    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            passwordImage.color = string.IsNullOrEmpty(value) ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
        }
    }

    public string RoomName
    {
        get { return roomNameText.text; }
        set { roomNameText.text = value; }
    }

    public string Owner
    {
        set { ownerText.text = value; }
    }

    public string Map
    {
        set { mapText.text = value; }
    }

    public int Players
    {
        set { playersText.text = value.ToString(); }
    }

    public int MaxPlayers
    {
        set { maxPlayersText.text = value.ToString(); }
    }
}
