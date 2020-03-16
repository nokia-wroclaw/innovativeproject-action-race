using UnityEngine;
using UnityEngine.UI;

public class GameCreatorPanel : MonoBehaviour
{
    [SerializeField] Text serverName;
    [SerializeField] Text password;
    [SerializeField] Text maxPlayers;
    [SerializeField] Toggle isVisibleInLobby;

    public string GetServerName()
    {
        return serverName.text;
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
