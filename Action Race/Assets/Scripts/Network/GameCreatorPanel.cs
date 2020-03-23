using UnityEngine;
using UnityEngine.UI;

public class GameCreatorPanel : MonoBehaviour
{
    [SerializeField] Text roomName;
    [SerializeField] Text password;
    [SerializeField] Text maxPlayers;
    [SerializeField] Toggle isVisibleInLobby;

    public string GetRoomName()
    {
        return roomName.text;
    }

    public string GetPassword()
    {
        return password.text;
    }

    public int GetMaxPlayers()
    {
        return int.Parse(maxPlayers.text);
    }

    public bool IsVisibleInLobby()
    {
        return isVisibleInLobby.isOn;
    }
}
