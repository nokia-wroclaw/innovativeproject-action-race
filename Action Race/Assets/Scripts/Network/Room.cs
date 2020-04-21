using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [SerializeField] Text roomNameText;
    [SerializeField] Text roomOwnerText;
    [SerializeField] Text roomModeText;
    [SerializeField] Text roomPasswordText;

    bool _roomPassword;
    string _roomName, _roomOwner, _roomMode;
    int _roomPlayers, _roomMaxPlayers;

    public string RoomName
    {
        get { return roomNameText.text; }
        set
        {
            _roomName = value;
            roomNameText.text = value;
        }
    }

    public string RoomOwner
    {
        get { return _roomOwner; }
        set
        {
            _roomOwner = value;
            roomOwnerText.text = value;
        }
    }

    public bool RoomPassword
    {
        get { return _roomPassword; }
        set
        {
            if (value) roomPasswordText.text = "Required";
            else roomPasswordText.text = "Not Required";
        }
    }

    public int RoomPlayers
    {
        get { return _roomPlayers; }
        set
        {
            _roomPlayers = value;
            SetRoomMode();
        }
    }

    public int RoomMaxPlayers
    {
        get { return _roomMaxPlayers; }
        set
        {
            _roomMaxPlayers = value;
            SetRoomMode();
        }
    }

    void SetRoomMode()
    {
        roomModeText.text = _roomPlayers + "/" + _roomMaxPlayers;
    }
}
