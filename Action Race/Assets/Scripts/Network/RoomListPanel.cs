using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : MonoBehaviour
{
    [Header("Room filter")]
    [SerializeField] InputField textFilter;
    [SerializeField] Toggle showFull;
    [SerializeField] Toggle showPrivate;

    [Header("Room list")]
    [SerializeField] RectTransform roomList;
    [SerializeField] GameObject roomPrefab;

    void Start()
    {
        TextFilter = "";
        ShowFull = true;
        ShowPrivate = true;
    }

    public void ClearRoomList()
    {
        foreach(RectTransform room in roomList)
        {
            Destroy(room.gameObject);
        }
    }

    public void AddRoom(string roomPassword, string roomName, string roomOwner, string roomMode, int roomPlayers, int roomMaxPlayers)
    {
        GameObject room = Instantiate(roomPrefab);
        RoomPanel roomPanel = room.GetComponent<RoomPanel>();
        roomPanel.RoomPassword = string.IsNullOrEmpty(roomPassword.Trim());
        roomPanel.RoomName = roomName;
        roomPanel.RoomOwner = roomOwner;
        roomPanel.RoomMode = roomMode;
        roomPanel.RoomPlayers = roomPlayers;
        roomPanel.RoomMaxPlayers = roomMaxPlayers;
        room.transform.SetParent(roomList, false);
    }

    public string TextFilter
    {
        get { return textFilter.text; }
        set { textFilter.text = value; }
    }

    public bool ShowFull
    {
        get { return showFull.isOn; }
        set { showFull.isOn = value; }
    }

    public bool ShowPrivate
    {
        get { return showPrivate.isOn; }
        set { showPrivate.isOn = value; }
    }
}
