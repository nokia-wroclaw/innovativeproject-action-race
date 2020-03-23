using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class RoomListController : MonoBehaviourPunCallbacks
{
    [SerializeField] JoinRoomPanel joinRoomPanel;

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
        joinRoomPanel.ClearRoomList();

        foreach(RoomInfo roomInfo in roomList)
        {
            string roomName = roomInfo.Name;
            string roomOwner = roomInfo.CustomProperties["Owner"].ToString();
            int roomPlayers = roomInfo.PlayerCount;
            int roomMaxPlayers = roomInfo.MaxPlayers;
            string roomPassword = roomInfo.CustomProperties["Password"].ToString();

            joinRoomPanel.AddRoom(roomName, roomOwner, roomPlayers, roomMaxPlayers, roomPassword);
        }
    }
}
