using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class JoinRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] RoomListPanel roomListPanel;

    List<RoomInfo> roomList = new List<RoomInfo>();

    public void JoinRoom(RoomPanel roomPanel)
    {
        Debug.Log("JoinRoom() " + roomPanel.RoomName);
        /*if (roomPanel.RoomPassword)
        {

        }
        else
        {
            
        }*/
        PhotonNetwork.JoinRoom(roomPanel.RoomName);
    }

    public void Refresh()
    {
        roomListPanel.ClearRoomList();

        foreach (RoomInfo roomInfo in roomList)
        {
            string roomPassword = roomInfo.CustomProperties["Password"].ToString();
            if (!roomListPanel.ShowPrivate && !string.IsNullOrEmpty(roomPassword.Trim())) continue;

            string roomName = roomInfo.Name;
            string roomOwner = roomInfo.CustomProperties["Owner"].ToString();
            string textFilter = roomListPanel.TextFilter.Trim();
            if (!roomName.ToLower().Contains(textFilter.ToLower()) && !roomOwner.ToLower().Contains(textFilter.ToLower())) continue;

            string roomMode = "";

            int roomPlayers = roomInfo.PlayerCount;
            int roomMaxPlayers = roomInfo.MaxPlayers;
            if (!roomListPanel.ShowFull && roomPlayers == roomMaxPlayers) continue;

            roomListPanel.AddRoom(roomPassword, roomName, roomOwner, roomMode, roomPlayers, roomMaxPlayers);
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
