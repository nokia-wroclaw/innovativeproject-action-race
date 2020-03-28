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

    public void AddRoom(string roomName, string roomOwner, int roomPlayers, int roomMaxPlayers, string roomPassword)
    {
        GameObject room = Instantiate(roomPrefab);
        RoomPanel roomPanel = room.GetComponent<RoomPanel>();
        roomPanel.RoomName = roomName;
        roomPanel.RoomOwner = roomOwner;
        roomPanel.RoomPlayers = roomPlayers;
        roomPanel.RoomMaxPlayers = roomMaxPlayers;
        roomPanel.RoomPassword = string.IsNullOrEmpty(roomPassword.Trim());
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
