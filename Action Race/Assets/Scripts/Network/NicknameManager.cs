using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NicknameManager : MonoBehaviour
{
    [SerializeField] InputField nickname;

    void Start()
    {
        int number = Random.Range(0, 10000);
        string nick = "Player" + number;
        nickname.text = nick;
    }

    public void ChangeNickName(string nickName)
    {
        NickName = nickName;
    }

    public string NickName
    {
        get
        {
            return PhotonNetwork.LocalPlayer.NickName;
        }

        set
        {
            PhotonNetwork.LocalPlayer.NickName = value;
        }
    }
}
