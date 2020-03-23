using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class RoomListController : MonoBehaviourPunCallbacks
{
    [SerializeField] JoinRoomPanel joinRoomPanel;
    [SerializeField] RoomListFilterPanel roomListFilterPanel;

    List<RoomInfo> roomList = new List<RoomInfo>();

    public void Refresh()
    {
        UpdateRoomList();
    }

    public void JoinRoom(RoomPanel roomPanel)
    {
        Debug.Log("JoinRoom() " + roomPanel.RoomName);
        PhotonNetwork.JoinRoom(roomPanel.RoomName);
    }

    void UpdateRoomList()
    {
        joinRoomPanel.ClearRoomList();

        foreach (RoomInfo roomInfo in roomList)
        {
            string roomName = roomInfo.Name;
            string roomOwner = roomInfo.CustomProperties["Owner"].ToString();
            string textFilter = roomListFilterPanel.TextFilter.Trim();
            if (!roomName.ToLower().Contains(textFilter.ToLower()) && !roomOwner.ToLower().Contains(textFilter.ToLower())) continue;

            int roomPlayers = roomInfo.PlayerCount;
            int roomMaxPlayers = roomInfo.MaxPlayers;
            if (!roomListFilterPanel.ShowFull && roomPlayers == roomMaxPlayers) continue;

            string roomPassword = roomInfo.CustomProperties["Password"].ToString();
            if (!roomListFilterPanel.ShowPrivate && roomPassword.Trim() != "") continue;

            joinRoomPanel.AddRoom(roomName, roomOwner, roomPlayers, roomMaxPlayers, roomPassword);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
        this.roomList = roomList;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
    }
}
