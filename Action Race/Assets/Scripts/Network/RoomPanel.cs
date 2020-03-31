using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    [SerializeField] GameObject roomPasswordGO;
    [SerializeField] Text roomNameText;
    [SerializeField] Text roomOwnerText;
    [SerializeField] Text roomModeText;
    [SerializeField] Text roomPlayersText;
    [SerializeField] Text roomMaxPlayersText;

    bool _roomPassword;
    string _roomName, _roomOwner, _roomMode;
    int _roomPlayer, _roomMaxPlayers;

    public bool RoomPassword
    {
        get { return _roomPassword; }
        set
        {
            _roomPassword = value;
            roomPasswordGO.SetActive(value);
        }
    }

    public string RoomName
    {
        get { return _roomName; }
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

    public string RoomMode
    {
        get { return _roomMode; }
        set
        {
            _roomMode = value;
            roomModeText.text = value;
        }
    }

    public int RoomPlayers
    {
        get { return _roomPlayer; }
        set
        {
            _roomPlayer = value;
            roomPlayersText.text = value.ToString();
        }
    }

    public int RoomMaxPlayers
    {
        get { return _roomMaxPlayers; }
        set
        {
            _roomMaxPlayers = value;
            roomMaxPlayersText.text = value.ToString();
        }
    }
}
