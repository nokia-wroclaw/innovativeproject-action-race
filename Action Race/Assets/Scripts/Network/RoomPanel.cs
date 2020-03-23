using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    [SerializeField] Text roomName;
    [SerializeField] Text roomOwner;
    [SerializeField] Text roomPlayers;
    [SerializeField] Text roomPassword;
    [SerializeField] Color passwordNotRequiredColor;
    [SerializeField] Color passwordRequiredColor;

    public string RoomName
    {
        get { return roomName.text; }
        set { roomName.text = value; }
    }

    public void SetRoomOwner(string roomOwner)
    {
        this.roomOwner.text = roomOwner;
    }

    public void SetRoomPlayers(int players, int maxPlayers)
    {
        this.roomPlayers.text = players + "/" + maxPlayers;
    }

    public void SetRoomPassword(string roomPassword)
    {
        roomPassword = roomPassword.Trim();
        if (roomPassword == null || roomPassword == "")
        {
            this.roomPassword.color = passwordNotRequiredColor;
            this.roomPassword.text = "NOT REQUIRED";
        }
        else
        {
            this.roomPassword.color = passwordRequiredColor;
            this.roomPassword.text = "REQUIRED";
        }
    }
}
