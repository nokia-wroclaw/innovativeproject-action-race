using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class JoinRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] RoomListInfo roomListInfo;

    List<RoomInfo> roomList = new List<RoomInfo>();

    public void JoinRoom(Room roomPanel)
    {
        Debug.Log("JoinRoom() " + roomPanel.RoomName);
        PhotonNetwork.JoinRoom(roomPanel.RoomName);
    }

    public void Refresh()
    {
        roomListInfo.ClearRoomList();

        foreach (RoomInfo roomInfo in roomList)
        {
            string roomPassword = roomInfo.CustomProperties["Password"] as string;
            if (!roomListInfo.ShowPrivate && !string.IsNullOrEmpty(roomPassword.Trim())) continue;

            string roomName = roomInfo.Name;
            string roomOwner = roomInfo.CustomProperties["Owner"].ToString();
            string textFilter = roomListInfo.TextFilter.Trim();
            if (!roomName.ToLower().Contains(textFilter.ToLower()) && !roomOwner.ToLower().Contains(textFilter.ToLower())) continue;

            int roomPlayers = roomInfo.PlayerCount;
            int roomMaxPlayers = roomInfo.MaxPlayers;
            if (!roomListInfo.ShowFull && roomPlayers == roomMaxPlayers) continue;

            roomListInfo.AddRoom(roomPassword, roomName, roomOwner, roomPlayers, roomMaxPlayers);
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
