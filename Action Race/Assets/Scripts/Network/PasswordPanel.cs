using UnityEngine;
using UnityEngine.UI;

public class PasswordPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject joinRoomPanelGO;
    [SerializeField] InputField passwordIF;
    [SerializeField] Text roomNameText;

    string _roomName;

    public string RoomName
    {
        get { return _roomName; }
        set
        {
            _roomName = value;
            roomNameText.text = value;
        }
    }

    public string Password
    {
        get { return passwordIF.text; }
    }

    public string PasswordExpected
    {
        get; set;
    }

    public void TryJoinRoom(string roomName, string passwordExpected)
    {
        RoomName = roomName;
        PasswordExpected = passwordExpected;

        joinRoomPanelGO.SetActive(false);
        gameObject.SetActive(true);
    }
}
