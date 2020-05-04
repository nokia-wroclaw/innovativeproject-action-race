using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class NickNameTemplatePanel : MonoBehaviour
{
    [SerializeField] Text nickNameText;

    Player player;

    public void SetUpTemplate(Player player)
    {
        this.player = player;
        nickNameText.text = player.NickName;
    }

    public Player GetPlayer()
    {
        return player;
    }
}
