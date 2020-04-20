using UnityEngine;
using UnityEngine.UI;

public class CreateRoomInfo : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] InputField roomNameIF;
    [SerializeField] InputField roomPasswordIF;
    [SerializeField] InputField roomMaxPlayersIF;
    [SerializeField] Toggle isVisibleT;

    [Header("References")]
    [SerializeField] NicknameManager nickNameManager;

    public void OnEnable()
    {
        RoomName = nickNameManager.NickName + "'s room";
    }

    public string RoomName
    {
        get { return roomNameIF.text; }
        set { roomNameIF.text = value.Trim(); }
    }

    public int RoomMaxPlayers
    {
        get { return int.Parse(roomMaxPlayersIF.text); }
    }

    public string RoomPassword
    {
        get { return roomPasswordIF.text; }
    }

    public bool IsVisible
    {
        get { return isVisibleT.isOn; }
    }
}
