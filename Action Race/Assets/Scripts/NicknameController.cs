using UnityEngine;
using UnityEngine.UI;

public class NicknameController : MonoBehaviour
{
    [SerializeField] InputField nickname;
    [SerializeField] GameCreatorPanel gameCreatorPanel;

    void Start()
    {
        int number = Random.Range(0, 10000);
        string nick = "Player" + number;
        nickname.text = nick;

        gameCreatorPanel.RoomName = nick + "'s room";
    }

    public string GetNickname()
    {
        return nickname.text;
    }
}
