using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomPanel : MonoBehaviour
{
    [SerializeField] RectTransform roomList;
    [SerializeField] GameObject roomPrefab;

    public void ClearRoomList()
    {
        foreach(RectTransform room in roomList)
        {
            Destroy(room.gameObject);
        }
    }

    public void AddRoom(string roomName, string roomOwner, int roomPlayers, int roomMaxPlayers, string roomPassword)
    {
        GameObject room = Instantiate(roomPrefab);
        RoomPanel roomPanel = room.GetComponent<RoomPanel>();
        roomPanel.RoomName = roomName;
        roomPanel.SetRoomOwner(roomOwner);
        roomPanel.SetRoomPlayers(roomPlayers, roomMaxPlayers);
        roomPanel.SetRoomPassword(roomPassword);
        room.transform.SetParent(roomList, false);
    }
}
