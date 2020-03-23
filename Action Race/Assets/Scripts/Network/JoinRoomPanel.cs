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

    public void AddRoom(string roomName, string roomOwner, int roomMaxPlayers, string roomPassword)
    {
        GameObject room = Instantiate(roomPrefab);
        RoomPanel roomPanel = room.GetComponent<RoomPanel>();
        roomPanel.SetRoomName(roomName);
        roomPanel.SetRoomOwner(roomOwner);
        roomPanel.SetRoomPlayers(roomMaxPlayers, roomMaxPlayers);
        roomPanel.SetRoomPassword(roomPassword);
        room.transform.SetParent(roomList, false);
    }
}
