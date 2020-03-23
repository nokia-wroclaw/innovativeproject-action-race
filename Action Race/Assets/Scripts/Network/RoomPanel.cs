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

    public void SetRoomName(string roomName)
    {
        this.roomName.text = roomName;
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
        if (roomPassword == null || roomPassword == "") this.roomPassword.text = "NOT REQUIRED";
        else this.roomPassword.text = "REQUIRED";
    }
}
