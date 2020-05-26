using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class JoinRoomController : MonoBehaviourPunCallbacks
{
    [Header("Custom Scripts References")]
    [SerializeField] ConnectionStatusPanel connectionStatusPanel;
    [SerializeField] JoinRoomPanel joinRoomPanel;
    [SerializeField] PasswordPanel passwordPanel;

    public List<RoomInfo> RoomsList { get; private set; }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RoomsList = roomList;
    }

    public void JoinRoom(RoomTemplate roomTemplate)
    {
        if (string.IsNullOrEmpty(roomTemplate.Password))
        {
            StartCoroutine(connectionStatusPanel.MessageFadeIn(ConnectionStatus.Join));
            PhotonNetwork.JoinRoom(roomTemplate.RoomName);
        }
        else 
            passwordPanel.TryJoinRoom(roomTemplate.RoomName, roomTemplate.Password);
    }

    public void Refresh()
    {
        joinRoomPanel.ClearRoomList();

        foreach (RoomInfo roomInfo in RoomsList)
        {
            if (roomInfo.RemovedFromList) continue;

            string password = roomInfo.CustomProperties[RoomProperty.Password] as string;
            if (!joinRoomPanel.ShowPrivate && !string.IsNullOrEmpty(password.Trim())) continue;

            string roomName = roomInfo.Name;
            string owner = roomInfo.CustomProperties[RoomProperty.Owner] as string;
            string textFilter = joinRoomPanel.Filter.Trim();
            if (!roomName.ToLower().Contains(textFilter.ToLower()) && !owner.ToLower().Contains(textFilter.ToLower())) continue;

            int players = roomInfo.PlayerCount;
            int maxPlayers = roomInfo.MaxPlayers;
            if (!joinRoomPanel.ShowFull && players == maxPlayers) continue;

            joinRoomPanel.AddRoom(password, roomName, owner, players, maxPlayers, JoinRoom);
        }
    }

    public string GetRandomRoomName()
    {
        List<RoomInfo> roomsListCopy = new List<RoomInfo>();
        foreach (RoomInfo roomInfo in RoomsList)
        {
            string password = roomInfo.CustomProperties[RoomProperty.Password] as string;
            int players = roomInfo.PlayerCount;
            int maxPlayers = roomInfo.MaxPlayers;
            if (roomInfo.RemovedFromList || !string.IsNullOrEmpty(password.Trim()) || players == maxPlayers) continue;

            roomsListCopy.Add(roomInfo);
        }

        int count = roomsListCopy.Count;
        if (count > 0)
            return roomsListCopy[Random.Range(0, count)].Name;
        else
            return null;
    }
}
