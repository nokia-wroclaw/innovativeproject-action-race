using UnityEngine;
using UnityEngine.UI;

public class RoomListInfo : MonoBehaviour
{
    [Header("Room filters")]
    [SerializeField] InputField textFilterIF;
    [SerializeField] Toggle showFullT;
    [SerializeField] Toggle showPrivateT;

    [Header("Rooms list")]
    [SerializeField] RectTransform roomList;
    [SerializeField] GameObject roomPrefab;

    void OnEnable()
    {
        TextFilter = "";
        ShowFull = true;
        ShowPrivate = true;
    }

    public void ClearRoomList()
    {
        foreach (RectTransform room in roomList)
        {
            Destroy(room.gameObject);
        }
    }

    public void AddRoom(string roomPassword, string roomName, string roomOwner, int roomPlayers, int roomMaxPlayers)
    {
        GameObject room = Instantiate(roomPrefab);
        Room roomPanel = room.GetComponent<Room>();
        roomPanel.RoomPassword = !string.IsNullOrEmpty(roomPassword.Trim());
        roomPanel.RoomName = roomName;
        roomPanel.RoomOwner = roomOwner;
        roomPanel.RoomPlayers = roomPlayers;
        roomPanel.RoomMaxPlayers = roomMaxPlayers;
        room.transform.SetParent(roomList, false);
    }

    public string TextFilter
    {
        get { return textFilterIF.text; }
        set { textFilterIF.text = value; }
    }

    public bool ShowFull
    {
        get { return showFullT.isOn; }
        set { showFullT.isOn = value; }
    }

    public bool ShowPrivate
    {
        get { return showPrivateT.isOn; }
        set { showPrivateT.isOn = value; }
    }
}
