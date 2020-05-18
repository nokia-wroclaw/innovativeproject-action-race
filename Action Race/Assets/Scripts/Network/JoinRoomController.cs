using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class JoinRoomController : MonoBehaviourPunCallbacks
{
    ConnectionStatusPanel csp;
    JoinRoomPanel jrp;

    List<RoomInfo> roomsList = new List<RoomInfo>();

    void Awake()
    {
        csp = FindObjectOfType<ConnectionStatusPanel>();
        jrp = FindObjectOfType<JoinRoomPanel>();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        roomsList = roomList;
    }

    public void JoinRoom(RoomTemplate roomTemplate)
    {
        //StartCoroutine(csp.MessageFadeIn(ConnectionStatus.Join));
        PhotonNetwork.JoinRoom(roomTemplate.RoomName);
    }

    public void Refresh()
    {
        jrp.ClearRoomList();

        foreach (RoomInfo roomInfo in roomsList)
        {
            if (roomInfo.RemovedFromList) continue;

            string password = roomInfo.CustomProperties[RoomProperty.Password] as string;
            if (!jrp.ShowPrivate && !string.IsNullOrEmpty(password.Trim())) continue;

            string roomName = roomInfo.Name;
            string owner = roomInfo.CustomProperties[RoomProperty.Owner] as string;
            string textFilter = jrp.Filter.Trim();
            if (!roomName.ToLower().Contains(textFilter.ToLower()) && !owner.ToLower().Contains(textFilter.ToLower())) continue;

            int players = roomInfo.PlayerCount;
            int maxPlayers = roomInfo.MaxPlayers;
            if (!jrp.ShowFull && players == maxPlayers) continue;

            jrp.AddRoom(password, roomName, owner, players, maxPlayers);
        }
    }
}
