using UnityEngine;
using UnityEngine.UI;

public class CreateRoomPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InputField roomNameIF;
    [SerializeField] InputField passwordIF;
    [SerializeField] Dropdown maxPlayersDropdown;
    [SerializeField] Toggle showInRoomListToggle;

    public string RoomName
    {
        get { return roomNameIF.text; }
    }

    public string Password
    {
        get { return passwordIF.text; }
    }

    public int MaxPlayers
    {
        get { return int.Parse(maxPlayersDropdown.options[maxPlayersDropdown.value].text); }
    }

    public bool ShowInRoomList
    {
        get { return showInRoomListToggle.isOn; }
    }
}
