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

    public bool RoomPassword
    {
        get { return RoomPassword; }
        set
        {
            RoomPassword = value;
            roomPasswordGO.SetActive(value);
        }
    }

    public string RoomName
    {
        get { return RoomName; }
        set
        {
            RoomName = value;
            roomNameText.text = value;
        }
    }

    public string RoomOwner
    {
        get { return RoomOwner; }
        set
        {
            RoomOwner = value;
            roomOwnerText.text = value;
        }
    }

    public string RoomMode
    {
        get { return RoomMode; }
        set
        {
            RoomMode = value;
            roomModeText.text = value;
        }
    }

    public int RoomPlayers
    {
        get { return RoomPlayers; }
        set
        {
            RoomPlayers = value;
            UpdateSlotsInfo();
        }
    }

    public int RoomMaxPlayers
    {
        get { return RoomMaxPlayers; }
        set
        {
            RoomMaxPlayers = value;
            UpdateSlotsInfo();
        }
    }

    void UpdateSlotsInfo()
    {
        roomPlayersText.text = RoomPlayers + "/" + RoomMaxPlayers;
    }
}
