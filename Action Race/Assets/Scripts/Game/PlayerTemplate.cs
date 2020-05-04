using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerTemplate : MonoBehaviour
{
    [SerializeField] Text statusText;
    [SerializeField] Text nickNameText;
    [SerializeField] Text pingText;

    Player player;

    public void SetUpTemplate(Player player)
    {
        this.player = player;
        SetNickName(player.NickName);
    }

    public void SetStatus(string status)
    {
        statusText.text = status;
    }

    public void SetPing(string ping)
    {
        pingText.text = ping;
    }

    public void SetNickName(string nickName)
    {
        nickNameText.text = nickName;
    }

    public Player GetPlayer()
    {
        return player;
    }
}
