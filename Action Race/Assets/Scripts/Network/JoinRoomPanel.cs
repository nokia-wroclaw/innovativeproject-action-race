using UnityEngine;
using UnityEngine.UI;
using System;

public class JoinRoomPanel : MonoBehaviour
{
    [Header("Room filters")]
    [SerializeField] InputField filterIF;
    [SerializeField] Toggle showFullToggle;
    [SerializeField] Toggle showPrivateToggle;

    [Header("Rooms list")]
    [SerializeField] RectTransform roomsList;
    [SerializeField] GameObject roomTemplateGO;

    void Start()
    {
        Filter = "";
        ShowFull = true;
        ShowPrivate = true;
    }

    public string Filter
    {
        get { return filterIF.text; }
        set { filterIF.text = value; }
    }

    public bool ShowFull
    {
        get { return showFullToggle.isOn; }
        set { showFullToggle.isOn = value; }
    }

    public bool ShowPrivate
    {
        get { return showPrivateToggle.isOn; }
        set { showPrivateToggle.isOn = value; }
    }

    public void ClearRoomList()
    {
        foreach (RectTransform room in roomsList)
            Destroy(room.gameObject);
    }

    public void AddRoom(string password, string roomName, string owner, int players, int maxPlayers, Action<RoomTemplate> clickMethod)
    {
        GameObject roomTemplate = Instantiate(roomTemplateGO, roomsList);
        RoomTemplate rt = roomTemplate.GetComponent<RoomTemplate>();
        rt.Password = password.Trim();
        rt.RoomName = roomName;
        rt.Owner = owner;
        rt.Players = players;
        rt.MaxPlayers = maxPlayers;

        rt.GetComponent<Button>().onClick.AddListener(delegate { clickMethod(rt); });
    }
}
