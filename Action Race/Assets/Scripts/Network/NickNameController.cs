using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NickNameController : MonoBehaviour
{
    [SerializeField] InputField nickNameIF;

    CreateRoomPanel crp;

    void Awake()
    {
        crp = FindObjectOfType<CreateRoomPanel>();
    }

    void Start()
    {
        string nickName;
        if (PlayerPrefs.HasKey("NickName"))
            nickName = PlayerPrefs.GetString("NickName");
        else
        {
            int number = Random.Range(0, 10000);
            nickName = "Player" + number;
        }

        nickNameIF.text = nickName;
        ChangeNickName(nickName);
    }

    public void ChangeNickName(string nickName)
    {
        PlayerPrefs.SetString("NickName", nickName);
        PhotonNetwork.LocalPlayer.NickName = nickName;

        crp.RoomName = nickName + "'s room";
    }
}
