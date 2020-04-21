using UnityEngine;
using UnityEngine.UI;

public class NicknameManager : MonoBehaviour
{
    [SerializeField] InputField nickname;
    [SerializeField] CreateRoomInfo createRoomInfo;

    void Start()
    {
        int number = Random.Range(0, 10000);
        string nick = "Player" + number;
        nickname.text = nick;

        createRoomInfo.RoomName = nick + "'s room";
    }

    public string GetNickname()
    {
        return nickname.text;
    }
}
