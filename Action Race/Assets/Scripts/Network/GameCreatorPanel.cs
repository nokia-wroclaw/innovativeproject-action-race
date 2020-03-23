using UnityEngine;
using UnityEngine.UI;

public class GameCreatorPanel : MonoBehaviour
{
    [SerializeField] InputField roomName;
    [SerializeField] InputField password;
    [SerializeField] InputField maxPlayers;
    [SerializeField] Toggle isVisibleInLobby;

    void Start()
    {
        MaxPlayers = 6;
    }

    public string RoomName
    {
        get { return roomName.text; }
        set { roomName.text = value; }
    }

    public int MaxPlayers
    {
        get { return int.Parse(maxPlayers.text); }
        set { maxPlayers.text = value.ToString(); }
    }

    public string GetPassword()
    {
        return password.text;
    }

    public bool IsVisibleInLobby()
    {
        return isVisibleInLobby.isOn;
    }
}
